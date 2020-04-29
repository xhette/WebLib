using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.DataLayer;
using WebLib.Models;
using WebLib.Models.ProviderPages;
using WebLib.Models.ReaderPages;

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
            ViewBag.Supply = supplyId;

            return PartialView("~/Views/ProviderPage/_OrdersBySupply.cshtml", model);
        }

        public ActionResult Shops()
        {
            List<ShopModel> model = providerContext.Shops().Select(c => (ShopModel)c).ToList();

            return View(model);
        }

		#region Shops
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
		#endregion

		#region Supplies and Orders
		[HttpGet]
        public ActionResult AddSupply()
        {
            SupplyAddModel model = new SupplyAddModel();
            model.Shops = providerContext.Shops().Select(c => (ShopModel)c).ToList();
            model.Supply.ShopId = model.Shops.FirstOrDefault().Id;

            return PartialView("~/Views/ProviderPage/_AddSupply.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddSupply(SupplyAddModel model)
        {
            if (ModelState.IsValid)
            {
                SupplyBs bs = new SupplyBs();
                var result = bs.Add((SupplyDTO)model.Supply);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Поставка успешно добавлена";

                    return RedirectToAction("Index", "ProviderPage");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return PartialView("~/Views/ProviderPage/_AddSupply.cshtml", model);
        }

        [HttpGet]
        public ActionResult AddOrder(int supplyId, int authorId = 0)
        {
            AddOrderModel model = new AddOrderModel();
            model.Authors = providerContext.Authors().Select(c => (AuthorModel)c).ToList();
            model.SelectedAuthor = model.Authors.FirstOrDefault().Id;
            if (authorId > 0)
            {
                model.SelectedAuthor = authorId;
            }

            model.SupplyId = supplyId;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrder(AddOrderModel model)
        {

            if (ModelState.IsValid)
            {
                OrderDTO order = new OrderDTO
                {
                    Book = new BookDTO
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        AuthorId = model.SelectedAuthor
                    },
                    Author = new AuthorDTO
                    {
                        Id = model.SelectedAuthor
                    },
                    Cost = model.Cost,
                    Supply = new SupplyDTO
                    {
                        Id = model.SupplyId
                    }
                };
                var result = providerContext.AddOrder(order);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Книга успешно добавлена";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }

                return RedirectToAction("Index", "ProviderPage");
            }

            model.Authors = providerContext.Authors().Select(c => (AuthorModel)c).ToList();
            if (model.SelectedAuthor == 0)
            {
                model.SelectedAuthor = model.Authors.FirstOrDefault().Id;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddAuthor (int supplyId)
        {
            AuthorModel model = new AuthorModel();
            ViewBag.Supply = supplyId;

            return PartialView("~/Views/ProviderPage/_AddAuthor.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddAuthor (AuthorModel model, int supplyId)
        {
            if (ModelState.IsValid)
            {
                var result = providerContext.AddAuthor((AuthorDTO)model);

                if (result > 0)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Автор успешно добавлен";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при добавлении автора";
                }

                return RedirectToAction("AddOrder", "ProviderPage", new { supplyId = supplyId, authorId = result });
            }

            return PartialView("~/Views/ProviderPage/_AddAuthor.cshtml", model);
        }
        #endregion

        public ActionResult Books(int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        #region BookSearch
        public ActionResult BooksByTitle()
        {
            List<BookViewModel> model;

            model = providerContext.Books().Select(c => (BookViewModel)c).ToList();
            return PartialView("~/Views/ProviderPage/_BooksByTitle.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult BookSearch(string symbols)
        {
            List<BookViewModel> model;

            if (!String.IsNullOrEmpty(symbols))
            {
                model = providerContext.Books().Where(c => c.Title.Contains(symbols)).Select(c => (BookViewModel)c).ToList();
            }
            else
            {
                model = providerContext.Books().Select(c => (BookViewModel)c).ToList();
            }

            ViewBag.TableHead = true;
            return PartialView("~/Views/ProviderPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByAuthor()
        {
            List<AuthorModel> model;
            providerContext = new ProviderPage(context);
            model = providerContext.Authors().Select(c => (AuthorModel)c).ToList();
            return PartialView("~/Views/ProviderPage/_BooksByAuthor.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorSearch(string symbols)
        {
            List<AuthorModel> model;
            model = providerContext.Authors().Where(c => c.Surname.Contains(symbols) || c.Name.Contains(symbols) || c.Patronymic.Contains(symbols))
                .Select(c => (AuthorModel)c).ToList();

            return PartialView("~/Views/ProviderPage/_AuthorSearch.cshtml", model);

        }
        public ActionResult BookSearchByAuthor(int authorId)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            if (authorId > 0)
            {
                model = providerContext.Books().Where(c => c.AuthorId == authorId).Select(c => (BookViewModel)c).ToList();
            }

            ViewBag.TableHead = false;
            return PartialView("~/Views/ProviderPage/_BookSearch.cshtml", model);
        }

        #endregion
    }
}