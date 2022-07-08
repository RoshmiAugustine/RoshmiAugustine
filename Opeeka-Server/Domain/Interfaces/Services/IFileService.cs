// -----------------------------------------------------------------------
// <copyright file="IFileService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IFileService
    {
        /// <summary>
        /// AddFile.
        /// </summary>
        /// <param name="fileDTO">fileDTO.</param>
        /// <returns>FileResponseDTO.</returns>
        FileResponseDTO AddFile(FileDTO fileDTO);

        /// <summary>
        /// DownloadFileFromBlob.
        /// </summary>
        /// <param name="blobReferenceKey">blobReferenceKey.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>byte.</returns>
        byte[] DownloadFileFromBlob(string blobReferenceKey, long agencyID, int userID, List<string> userRole);

        /// <summary>
        /// SaveProfilePicAsync.
        /// </summary>
        /// <param name="uploadfile">uploadfile.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>FileResponseDTO.</returns>
        Task<FileResponseDTO> SaveProfilePicAsync(IFormFile uploadfile, long agencyID, int userID, List<string> userRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobReferenceKey"></param>
        /// <param name="agencyID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool DeleteFileFromBlob(string blobReferenceKey, long agencyID, int userID, List<string> userRole);
    }
}
