using Opeeka.PICS.Domain.DTO;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IAzureADB2CService
    {
        /// <summary>
        /// create user in b2c
        /// </summary>
        /// <param name="usersDTO"></param>
        /// <param name="systemRoleName"></param>
        /// <returns>string</returns>
        Tuple<string,bool> BuildUserSignUpToken(HelperDetailsDTO usersDTO, string systemRoleName,int UserID);

        /// <summary>
        /// Update User Extensions
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <param name="systemRoleName"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<string> UpdateUserExtensions(HelperDTO helperDTO, string systemRoleName);

        Task<string> UpdateUserAgencyExtension(UsersDTO helperDTO);

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="b2cUserId"></param>
        /// <returns></returns>
        Task<string> DeleteUserById(string b2cUserId);

        /// <summary>
        /// Reset Password And Send Mail
        /// </summary>
        /// <param name="usersDTO"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        string ResetPasswordAndSendMail(HelperDetailsDTO usersDTO,int UserID);
    }
}
