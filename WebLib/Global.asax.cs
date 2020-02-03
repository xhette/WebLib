using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebLib.DataLayer;
using WebMatrix.WebData;

namespace WebLib
{
	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			#region Инициализация EF и SMP
			Database.SetInitializer (new MigrateDatabaseToLatestVersion<LibDbContext, DataLayer.Migrations.Configuration> ());

			if (!WebSecurity.Initialized)
			{
				WebSecurity.InitializeDatabaseConnection ("LibDbConnection", "Users", "Id", "Name", autoCreateTables: true);
			}

			#endregion
		}
	}
}
