using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.DataLayer;

namespace WebLib.Controllers
{
    public class ProviderPageController : Controller
    {
        private ProviderPage providerContext;
        private LibContext context;
        private int userId;
        private int readerId;

        public ProviderPageController()
        {
            //userId = WebSecurity.GetUserId(User.Identity.Name);
            //if (userId == 0) 
            userId = 101;

            context = new LibContext();
            providerContext = new ProviderPage(context);

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