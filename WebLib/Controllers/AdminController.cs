using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.Models;

namespace WebLib.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Authors()
        {
            return View();
        }

        public ActionResult AuthorsList(string symbols = "")
        {
            AuthorBs author = new AuthorBs();
            List<AuthorModel> model = author.GetList().Where(
                c => c.Name.Contains(symbols)
                || c.Surname.Contains(symbols)
                || c.Patronymic.Contains(symbols))
                .Select(c => (AuthorModel)c).ToList();

            return PartialView("~/Views/Admin/_AuthorsList.cshtml", model);
        }
    }
}