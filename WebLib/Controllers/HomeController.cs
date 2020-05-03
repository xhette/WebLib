using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables;
using WebMatrix.WebData;

namespace WebLib.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
    {
        public ActionResult Index()
        {

			if (User.Identity.IsAuthenticated)
			{
				SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
				if (roles.IsUserInRole(User.Identity.Name, "admin"))
					return RedirectToAction("Index", "Admin");

				if (roles.IsUserInRole(User.Identity.Name, "librarian"))
					return RedirectToAction("Index", "LibrarianPage");

				if (roles.IsUserInRole(User.Identity.Name, "provider"))
					return RedirectToAction("Index", "ProviderPage");

				if (roles.IsUserInRole(User.Identity.Name, "reader"))
					return RedirectToAction("Index", "ReaderPage");
			} 
			return RedirectToAction("Index", "Login");
		}
    }
}