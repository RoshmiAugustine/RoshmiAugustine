// -----------------------------------------------------------------------
// <copyright file="AspNetUserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Data;
using System;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Linq;
using System.Collections.Generic;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AspNetUserRepository : IAspNetUserRepository
    {
        private readonly OpeekaDBContext _context;
        public AspNetUserRepository(OpeekaDBContext context)
        {
            _context = context;
        }

        #region createuser
        public async Task<IdentityResult> CreateAsync(AspNetUser user)
        {
            try
            {
                _context.AspNetUsers.Add(user);
                var customer = await _context.SaveChangesAsync();
                if (customer != 0)
                {
                    {
                        return IdentityResult.Success;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }


            return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." }));
        }
        #endregion

        public async Task<IdentityResult> UpdateAsync(AspNetUser user)
        {
            try
            {
                _context.AspNetUsers.Update(user);
                var customer = await _context.SaveChangesAsync();
                if (customer != 0)
                {
                    {
                        return IdentityResult.Success;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }


            return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." }));
        }
        public async Task<IdentityResult> DeleteAsync(AspNetUser user)
        {
            // using (IDbConnection conn = Connection)
            // {
            //     string sql = "DELETE FROM dbo.CustomUser WHERE Id = @Id";
            //     conn.Open();
            //     int rows = await conn.ExecuteAsync(sql, new { user.Id });
            //     if (rows > 0)
            //     {
            //         return IdentityResult.Success;
            //     }
            // }
            var removed = Task.FromResult(_context.Remove(user));
            var customer = await _context.SaveChangesAsync();
            if (customer != 0)
            {
                return IdentityResult.Success;
            }
            return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." }));
        }


        public async Task<AspNetUser> FindByIdAsync(Guid userId, long agencyId)
        {
            // using (IDbConnection conn = Connection)
            // {
            //     string sql = "SELECT * " +
            //                 "FROM dbo.CustomUsers " +
            //                 "WHERE Id = @Id;";
            //     conn.Open();
            //     return await conn.QuerySingleOrDefaultAsync<Users>(sql, new
            //     {
            //         Id = userId
            //     });
            // }
            var user = _context.AspNetUsers.Where(a => a.Id == userId.ToString() && (a.AgencyId == agencyId || a.AgencyId == 0 || a.AgencyId == null)).FirstOrDefault();
            return await Task.FromResult(user);
        }


        public async Task<AspNetUser> FindByNameAsync(string userName, long agencyId)
        {
            // using (IDbConnection conn = Connection)
            // {
            //     string sql = "SELECT * " +
            //                 "FROM dbo.CustomUser " +
            //                 "WHERE UserName = @UserName;";
            //     conn.Open();
            //     return await conn.QuerySingleOrDefaultAsync<Users>(sql, new
            //     {
            //         UserName = userName
            //     });
            // }
            var user = _context.AspNetUsers.Where(a => a.UserName.ToLower() == userName.ToLower() && (a.AgencyId == agencyId || a.AgencyId == 0 || a.AgencyId == null)).FirstOrDefault();
            return await Task.FromResult(user);
        }
        public async Task<AspNetUser> FindByEmailAsync(string email, long agencyId)
        {
            var user = _context.AspNetUsers.Where(a => a.Email.ToLower() == email.ToLower() && a.IsActive == true &&
            (a.AgencyId == agencyId || a.AgencyId == 0 || a.AgencyId == null)).FirstOrDefault();
            return await Task.FromResult(user);
        }

        public List<AspNetUser> GetUserList(int agencyId)
        {
            var user = _context.AspNetUsers.Where(a => a.AgencyId == agencyId).ToList();
            return user;
        }

        // public async Task<IdentityResult> SetUserRole(UserRoleMapping userRole)
        // {     
        //     try
        //     {
        //         _context.UserRoleMapping.Add(userRole);
        //         var customer = await _context.SaveChangesAsync(); 
        //           if (customer != 0)
        //             {
        //                 {
        //                     return IdentityResult.Success;
        //                 }
        //             }
        //     }     
        //     catch(Exception e)
        //     {
        //         throw e;
        //     }
        //     return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Could not insert user role {userRole.UserID}." }));
        // }
    }
}