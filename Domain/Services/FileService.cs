// -----------------------------------------------------------------------
// <copyright file="FileService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Services
{
    public class FileService : BaseService, IFileService
    {
        private readonly IFileRepository fileRepository;
        private readonly IConfiguration configuration;
        private IUserRepository userRepository;
        private IUserProfileRepository userProfileRepository;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        public FileService(LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IFileRepository fileRepository, IConfiguration configuration, IUserRepository userRepository, IUserProfileRepository userProfileRepository)

            : base(configRepo, httpContext)
        {
            this.localize = localizeService;
            this.fileRepository = fileRepository;
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// AddFile.
        /// </summary>
        /// <param name="fileDTO">fileDTO.</param>
        /// <returns>FileResponseDTO.</returns>
        public FileResponseDTO AddFile(FileDTO fileDTO)
        {
            try
            {
                FileResponseDTO fileResponseDTO = new FileResponseDTO();

                long fileID = this.fileRepository.AddFile(fileDTO);
                if (fileID != 0)
                {
                    fileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    fileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    fileResponseDTO.fileID = fileID;
                }
                else
                {
                    fileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    fileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return fileResponseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DownloadFileFromBlob.
        /// </summary>
        /// <param name="blobReferenceKey">blobReferenceKey</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>byte.</returns>
        public byte[] DownloadFileFromBlob(string blobReferenceKey, long agencyID, int userID, List<string> userRole)
        {
            var ms = new MemoryStream();
            var storageConnectionString = this.configuration.GetValue<string>("AzureStorageConnectionString");
            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(PCISEnum.AzureConstants.Container);
                CloudBlobDirectory profileImagesFolder = container.GetDirectoryReference(PCISEnum.AzureConstants.ContainerFolder);
                CloudBlobDirectory agencyFolder = null;
                if (userRole.Contains(PCISEnum.Roles.SuperAdmin))
                {
                    agencyFolder = profileImagesFolder.GetDirectoryReference(PCISEnum.AzureConstants.SuperAdminFolder);
                }
                else
                {
                    agencyFolder = profileImagesFolder.GetDirectoryReference(agencyID.ToString());
                }
                CloudBlobDirectory userFolder = agencyFolder.GetDirectoryReference(userID.ToString());
                var blockBlob = userFolder.GetBlockBlobReference(blobReferenceKey);

                using (ms)
                {
                    if (blockBlob.Exists())
                    {
                        blockBlob.DownloadToStream(ms);
                        return ms.ToArray();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// SaveProfilePicAsync.
        /// </summary>
        /// <param name="uploadfile">uploadfile.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>FileResponseDTO.</returns>
        public async Task<FileResponseDTO> SaveProfilePicAsync(IFormFile uploadfile, long agencyID, int userID, List<string> userRole)
        {
            try
            {
                FileResponseDTO fileResponseDTO = new FileResponseDTO();
                var storageConnectionString = this.configuration.GetValue<string>("AzureStorageConnectionString");
                Guid newGuidValue = Guid.NewGuid();
                FileInfo fi = new FileInfo(uploadfile.FileName);
                string filenameextension = fi.Extension;
                string newfileName = newGuidValue.ToString() + filenameextension;

                if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(PCISEnum.AzureConstants.Container);
                    await container.CreateIfNotExistsAsync();

                    CloudBlobDirectory profileImagesFolder = container.GetDirectoryReference(PCISEnum.AzureConstants.ContainerFolder);
                    CloudBlobDirectory agencyFolder = null;
                    if (userRole.Contains(PCISEnum.Roles.SuperAdmin))
                    {
                        agencyFolder = profileImagesFolder.GetDirectoryReference(PCISEnum.AzureConstants.SuperAdminFolder);
                    }
                    else
                    {
                        agencyFolder = profileImagesFolder.GetDirectoryReference(agencyID.ToString());
                    }
                    CloudBlobDirectory userFolder = agencyFolder.GetDirectoryReference(userID.ToString());
                    CloudBlockBlob blockblob = userFolder.GetBlockBlobReference(newfileName);
                    using (var outputStream = await blockblob.OpenWriteAsync())
                    {
                        uploadfile.CopyTo(outputStream);
                    }

                }
                else
                {
                    fileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.ImageUploadFailed;
                    fileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.ImageUploadFailed);
                    return fileResponseDTO;
                }

                FileDTO fileDTO = new FileDTO();
                fileDTO.AgencyID = agencyID;
                fileDTO.AzureID = newGuidValue;
                fileDTO.FileName = uploadfile.FileName;
                fileDTO.AzureFileName = newfileName;
                if (userRole.Contains(PCISEnum.Roles.SuperAdmin))
                {
                    fileDTO.Path = PCISEnum.AzureConstants.Container + "/" + PCISEnum.AzureConstants.ContainerFolder + "/" + PCISEnum.AzureConstants.SuperAdminFolder + "/" + userID.ToString() + "/";
                }
                else
                {
                    fileDTO.Path = PCISEnum.AzureConstants.Container + "/" + PCISEnum.AzureConstants.ContainerFolder + "/" + agencyID.ToString() + "/" + userID.ToString() + "/";
                }
                var result = DeleteAlreadyExistingFile(agencyID, userID, userRole);
                if (result)
                {
                    fileResponseDTO = this.AddFile(fileDTO);
                    fileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.ImageUploadSuccess;
                    fileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.ImageUploadSuccess);
                    return fileResponseDTO;
                }
                else
                {
                    fileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    fileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                    return fileResponseDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAlreadyExistingFile(long agencyID, int userID, List<string> userRole)
        {
            UserProfileDTO userProfileDTO = userRepository.GetUserProfilePicDetails(userID);
            if (userProfileDTO != null)
            {
                string blobReferenceKey = userProfileDTO.AzureFileName;
                long fileID = userProfileDTO.ImageFileID;

                Opeeka.PICS.Domain.Entities.File file = this.fileRepository.GetFile(fileID);

                UserProfile userProfile = new UserProfile();
                userProfile.UserID = userID;
                userProfile.UserProfileID = userProfileDTO.UserProfileID;
                userProfile.ImageFileID = userProfileDTO.ImageFileID;

                bool isSuccess = DeleteFileFromBlob(blobReferenceKey, agencyID, userID, userRole);
                if (isSuccess)
                {
                    userProfileRepository.DeleteUserProfile(userProfile);
                    fileRepository.DeleteFile(file);
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteFileFromBlob(string blobReferenceKey, long agencyID, int userID, List<string> userRole)
        {
            try
            {
                var storageConnectionString = this.configuration.GetValue<string>("AzureStorageConnectionString");
                if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
                {
                    var blobClient = storageAccount.CreateCloudBlobClient();
                    var container = blobClient.GetContainerReference(PCISEnum.AzureConstants.Container);
                    CloudBlobDirectory profileImagesFolder = container.GetDirectoryReference(PCISEnum.AzureConstants.ContainerFolder);
                    CloudBlobDirectory agencyFolder = null;
                    if (userRole.Contains(PCISEnum.Roles.SuperAdmin))
                    {
                        agencyFolder = profileImagesFolder.GetDirectoryReference(PCISEnum.AzureConstants.SuperAdminFolder);
                    }
                    else
                    {
                        agencyFolder = profileImagesFolder.GetDirectoryReference(agencyID.ToString());
                    }
                    CloudBlobDirectory userFolder = agencyFolder.GetDirectoryReference(userID.ToString());
                    var blockBlob = userFolder.GetBlockBlobReference(blobReferenceKey);
                    if (blockBlob.Exists())
                    {
                        blockBlob.DeleteIfExistsAsync();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
