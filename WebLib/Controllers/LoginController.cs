using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.Models;
using WebMatrix.WebData;

namespace WebLib.Controllers
{
    public class LoginController : Controller
	{
		[HttpGet]
		[AllowAnonymous]
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

			LoginModel model = new LoginModel();
			HttpCookie existingCookie = Request.Cookies["info_username"];
			if (existingCookie != null)
			{
				model.Login = existingCookie.Value;
				model.RememberMe = true;
			}
			else
			{
				model.RememberMe = false;
			}

			return View(model);
        }

		[HttpPost]
		[AllowAnonymous]
		public ActionResult Index(LoginModel model)
        {
			HttpCookie existingCookie = Request.Cookies["info_username"];

			if (model.RememberMe)
			{
				if (existingCookie != null)
				{
					existingCookie.Expires = DateTime.Today.AddDays(7);
				}
				else
				{
					HttpCookie newCookie = new HttpCookie("info_username", model.Login);
					newCookie.Expires = DateTime.Today.AddDays(7);
					Response.Cookies.Add(newCookie);
				}
			}
			else
			{
				if (existingCookie != null)
				{
					Response.Cookies["info_username"].Expires = DateTime.Now.AddDays(-1);
				}
			}

			if (ModelState.IsValid && WebSecurity.Login(model.Login, model.Password, persistCookie: model.RememberMe))
			{
				var user = Membership.GetUser(model.Login);
				if (user != null)
				{
					if (Membership.ValidateUser(model.Login, model.Password))
					{
						Session["UserLogin"] = model.Login;
						FormsAuthentication.SetAuthCookie(user.UserName, true);
						SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
						if (roles.IsUserInRole(model.Login, "admin"))
							return RedirectToAction("Index", "Admin");

						if (roles.IsUserInRole(model.Login, "librarian"))
							return RedirectToAction("Index", "LibrarianPage");

						if (roles.IsUserInRole(model.Login, "provider"))
							return RedirectToAction("Index", "ProviderPage");

						if (roles.IsUserInRole(model.Login, "reader"))
							return RedirectToAction("Index", "ReaderPage");
					}
				}
			}

			return RedirectToAction("Index", "Login");
		}

		public ActionResult LogOff()
		{
			Session.Clear();
			Session.Abandon();
			WebSecurity.Logout();

			return RedirectToAction("Index", "Login");
		}
	}
}
