using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
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

			#region Инициализация SMP и создание ролей

			if (!WebSecurity.Initialized)
			{
				WebSecurity.InitializeDatabaseConnection ("LibDbConnection", "Users", "Id", "Name", autoCreateTables: true);
				//Utils.CreateAccounts.Up();
			}
			#endregion
		}
	}
}
