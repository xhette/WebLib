using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
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
            userId = 4;

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

        [HttpGet]
        public ActionResult AddShop()
        {
            ShopModel model = new ShopModel();

            return PartialView("~/Views/ProviderPage/_AddShop.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddShop(ShopModel model)
        {
            if (ModelState.IsValid)
            {
                ShopBs bs = new ShopBs();
                var result = bs.Add((ShopDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Магазин успешно добавлен";

                    return RedirectToAction("Shops", "ProviderPage");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return PartialView("~/Views/ProviderPage/_AddShop.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditShop(int id)
        {
            ShopModel model = (ShopModel)providerContext.Shops().FirstOrDefault(c => c.Id == id);

            return PartialView("~/Views/ProviderPage/_EditShop.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditShop(ShopModel model)
        {
            if (ModelState.IsValid)
            {
                ShopBs bs = new ShopBs();
                var result = bs.Update((ShopDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";

                    return RedirectToAction("Shops", "ProviderPage");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return PartialView("~/Views/ProviderPage/_EditShop.cshtml", model);
        }

        public ActionResult DeleteShop(int id)
        {
            if (ModelState.IsValid)
            {
                ShopBs bs = new ShopBs();
                var result = bs.Delete(id);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }
            return RedirectToAction("Shops", "ProviderPage");
        }
    }
}