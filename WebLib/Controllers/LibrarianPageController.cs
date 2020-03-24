using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.DataLayer;

namespace WebLib.Controllers
{
    public class LibrarianPageController : Controller
    {
        private LibrarianPage librarianContext;
        private LibContext context;
        private int userId;
        private int readerId;

        public LibrarianPageController()
        {
            //userId = WebSecurity.GetUserId(User.Identity.Name);
            //if (userId == 0) 
            userId = 100;

            context = new LibContext();
            librarianContext = new LibrarianPage(context);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);

            if (reader != null)
            {
                readerId = reader.Id;
            }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}