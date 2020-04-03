using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.DataLayer;
using WebLib.Models;
using WebLib.Models.ProviderPages;

namespace WebLib.Controllers
{
    public class ProviderPageController : Controller
    {
        private ProviderPage providerContext;
        private LibContext context;
        private int userId;
        private int providerId;

        public ProviderPageController()
        {
            //userId = WebSecurity.GetUserId(User.Identity.Name);
            //if (userId == 0) 
            userId = 101;

            context = new LibContext();
            providerContext = new ProviderPage(context);

            var provider = context.Providers.FirstOrDefault(c => c.UserId == userId);

            if (provider != null)
            {
                providerId = provider.Id;
            }
        }

        public ActionResult SidebarPartial()
        {
            ProviderModel model = (ProviderModel)providerContext.ProviderInfo(providerId);

            return PartialView("~/Views/ProviderPage/_ProviderInfo.cshtml", model);
        }

        public ActionResult Index()
        {
            List<SupplyViewModel> model = providerContext.SuppliesList().Select(c => (SupplyViewModel)c).ToList();

            return View(model);
        }

        public ActionResult OrderList(int supplyId)
        {
            List<OrderModel> model = providerContext.Orders(supplyId).Select(c => (OrderModel)c).ToList();

            return PartialView("~/Views/ProviderPage/_OrdersBySupply.cshtml", model);
        }

        public ActionResult Shops()
        {
            List<ShopModel> model = providerContext.Shops().Select(c => (ShopModel)c).ToList();

            return View(model);
        }
    }
}