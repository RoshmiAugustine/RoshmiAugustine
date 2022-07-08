using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        /// <summary>
        /// AddFile.
        /// </summary>
        /// <param name="fileDTO"></param>
        /// <returns>long.</returns>
        long AddFile(FileDTO fileDTO);
        void DeleteFile(File fileID);

        File GetFile(long fileID);
    }
}
