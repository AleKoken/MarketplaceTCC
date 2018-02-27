using MarketplaceTCC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Configuration;

namespace MarketplaceTCC.Classes
{
    public class UserHelper
    {
        public class UsersHelper : IDisposable
        {
            private static ApplicationDbContext userContext = new ApplicationDbContext();
            private static Context db = new Context();


            //DELETAR USUÁRIOS
            public static bool DeleteUser(string userName)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var userASP = userManager.FindByEmail(userName);
                if (userASP == null)
                {
                    return false;
                }

                var response = userManager.Delete(userASP);
                return response.Succeeded;
            }


            //atualizar usuários
            public static bool UpdateUserName(string currentUserName, string newUserName)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var userASP = userManager.FindByEmail(currentUserName);
                if (userASP == null)
                {
                    return false;
                }

                userASP.UserName = newUserName;
                userASP.Email = newUserName;
                var response = userManager.Update(userASP);
                return response.Succeeded;
            }


            public static void CheckRole(string roleName)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

                // Check to see if Role Exists, if not create it
                if (!roleManager.RoleExists(roleName))
                {
                    roleManager.Create(new IdentityRole(roleName));
                }
            }


            public static void CheckSuperUser()
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var email = WebConfigurationManager.AppSettings["AdminUser"];
                var password = WebConfigurationManager.AppSettings["AdminPassWord"];
                var userASP = userManager.FindByName(email);
                if (userASP == null)
                {
                    CreateUserASP(email, "Admin", password);
                    return;
                }

                userManager.AddToRole(userASP.Id, "Admin");
            }


            public static void CreateUserASP(string email, string roleName, string password)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

                var userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };

                userManager.Create(userASP, password);
                userManager.AddToRole(userASP.Id, roleName);
            }


            public void Dispose()
            {
                userContext.Dispose();
                db.Dispose();
            }
        }
    }
}