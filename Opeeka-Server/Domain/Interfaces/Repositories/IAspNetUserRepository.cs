
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAspNetUserRepository
    {
        Task<IdentityResult> CreateAsync(AspNetUser user);
        Task<IdentityResult> DeleteAsync(AspNetUser user);
        Task<AspNetUser> FindByIdAsync(Guid userId, long agencyId);
        Task<AspNetUser> FindByNameAsync(string userName, long agencyId);
        Task<IdentityResult> UpdateAsync(AspNetUser user);
        Task<AspNetUser> FindByEmailAsync(string email, long agencyId);
        List<AspNetUser> GetUserList(int agencyId);
    }
}