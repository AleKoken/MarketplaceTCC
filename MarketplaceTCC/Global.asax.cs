using MarketplaceTCC.Classes;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MarketplaceTCC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Context, Migrations.Configuration>());
            CheckRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRolesAndSuperUser()
        {
            UserHelper.UsersHelper.CheckRole("Admin");
            UserHelper.UsersHelper.CheckRole("Cliente");
            UserHelper.UsersHelper.CheckRole("Vendedor");
            UserHelper.UsersHelper.CheckSuperUser();
        }
    }
}
