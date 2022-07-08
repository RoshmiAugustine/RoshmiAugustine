using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {
        /// <summary>
        /// AddUserProfile.
        /// </summary>
        /// <param name="userProfileDTO">userProfileDTO.</param>
        /// <returns>int.</returns>
        int AddUserProfile(UserProfileDTO userProfileDTO);
        void DeleteUserProfile(UserProfile userProfile);
        Task<UserProfile> GetUserProfileByID(int userID);
    }
}
