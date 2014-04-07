using BM.Models;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BM.DataAccess
{
    public interface IUserRepository
    {
        bool AddUserToRole(string userId, string roleName);
        void ClearUserRoles(string userId);
        bool CreateRole(string name);
        bool CreateUser(User user, string password);
        bool RoleExists(string name);
        RoleManager<IdentityRole> RoleManager { get; }
        UserManager<User> UserManager { get; }
    }
}
