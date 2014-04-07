using BM.DataAccess;
using BM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BM.DataAccess
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() : this(new DbContext())
        {
        }

        public UserRepository(DbContext context)
        {
            UserManager = new UserManager<User>(new UserStore<User>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public UserManager<User> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public bool RoleExists(string name)
        {
            return RoleManager.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var idResult = RoleManager.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public bool CreateUser(User user)
        {
            var idResult = UserManager.Create(user);
            return idResult.Succeeded;
        }

        public bool CreateUser(User user, string password)
        {
            var idResult = UserManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = UserManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        //public bool GetAllRoles()
        //{
        //    return DbContext.Set<Role>();
        //}


        public void ClearUserRoles(string userId)
        {
            var user = UserManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(userId, role.Role.Name);
            }
        }
    }
}