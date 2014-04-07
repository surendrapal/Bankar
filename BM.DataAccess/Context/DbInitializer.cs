using BM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BM.DataAccess
{
    public class DbInitializer :DropCreateDatabaseIfModelChanges<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            var identityManagerRepository = new UserRepository();

            string userName = "admin";
            string password = "123456";
            string role = "Administrator";
            var user = new User() { UserName = userName, Email = "admin@admin.com", FirstName = "Administrator", LastName =" " };

            var adminresult = identityManagerRepository.CreateUser(user, password);
            var userresult = identityManagerRepository.CreateRole(role);
            //Add User Admin to Role Admin
            if (adminresult && userresult)
            {
                var result = identityManagerRepository.AddUserToRole(user.Id, role);
            }
            context.Locations.Add(new Location
             {
                 Name = "Primary",
                 ParentId = null
             });
            context.Groups.Add(new Group
            {
                Name = "Primary",
                Description = "Primary",
                ParentId = null
            });
            context.SaveChanges();
        }
    }
}