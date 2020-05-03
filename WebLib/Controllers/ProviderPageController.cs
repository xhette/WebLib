using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.DataLayer;
using WebLib.Models;
using WebLib.Models.ProviderPages;
using WebLib.Models.ReaderPages;
using WebMatrix.WebData;

namespace WebLib.Controllers
{
    [Authorize(Roles = "provider")]
    public class ProviderPageController : Controller
    {
        private SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
        private SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

        private ProviderPage providerContext;
        private LibContext context;

        public ProviderPageController()
        {
            context = new LibContext();
            providerContext = new ProviderPage(context);
        }

        public ActionResult SidebarPartial()
        {
            ProviderModel model = new ProviderModel();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var provider = context.Providers.FirstOrDefault(c => c.UserId == userId);

            if (provider != null)
            {
                int providerId = provider.Id;


                model = (ProviderModel)providerContext.ProviderInfo(providerId);
            }

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

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            BookEditModel model = (BookEditModel)providerContext.Books().FirstOrDefault(c => c.BookId == id);
            model.Authors = providerContext.Authors().Select(c => (AuthorModel)c).ToList();
            LibraryBs libraryBs = new LibraryBs();
            model.Libraries = libraryBs.GetList().Select(c => (LibraryModel)c).ToList();

            if (model.Book.DepartmentId.HasValue && model.Book.DepartmentId.Value > 0)
            {
                DepartmentBs bs = new DepartmentBs();
                if (model.SelectedLib == 0)
                {
                    model.SelectedLib = bs.GetById(model.Book.DepartmentId.Value).LibraryId;
                }
                var departments = bs.GetList().Where(c => c.LibraryId == model.SelectedLib).ToList();

                if (departments != null)
                {
                    model.Departments = departments.Select(c => (DepartmentModel)c).ToList();
                }
            }
            else
            {
                DepartmentBs bs = new DepartmentBs();

                var lib = model.Libraries.FirstOrDefault();

                if (lib != null)
                {
                    model.SelectedLib = lib.Id;
                    var departments = bs.GetList().Where(c => c.LibraryId == model.SelectedLib).ToList();

                    if (departments != null)
                    {
                        model.Departments = departments.Select(c => (DepartmentModel)c).ToList();
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBook(BookEditModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Book.DepartmentId == 0)
                    model.Book.DepartmentId = null;

                BookBs bs = new BookBs();
                var result = bs.Update((BookDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";

                    return RedirectToAction("Books", "ProviderPage");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            model.Authors = providerContext.Authors().Select(c => (AuthorModel)c).ToList();
            DepartmentBs dbs = new DepartmentBs();
            model.Departments = dbs.GetList().Where(c => c.LibraryId == model.SelectedLib).Select(c => (DepartmentModel)c).ToList();
            return View(model);
        }

        public ActionResult DeleteBook(int id)
        {
            if (ModelState.IsValid)
            {
                BookBs bs = new BookBs();
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
            return RedirectToAction("Books", "ProviderPage");
        }

        public ActionResult DepartmentListByLib(int id = 0)
        {
            DepartmentBs bs = new DepartmentBs();

            if (id != 0)
            {
                List<DepartmentModel> model = bs.GetList().Where(c => c.LibraryId == id).Select(c => (DepartmentModel)c).ToList();

                return PartialView("~/Views/Shared/_DepartmentDropdown.cshtml", model);
            }
            else
            {
                int libId = 0;
                LibraryBs lbs = new LibraryBs();
                var lib = lbs.GetList().FirstOrDefault();

                if (lib != null)
                {
                    libId = lib.Id;
                }

                List<DepartmentModel> model = bs.GetList().Where(c => c.LibraryId == id).Select(c => (DepartmentModel)c).ToList();
                return PartialView("~/Views/Shared/_DepartmentDropdown.cshtml", model);
            }

        }
        #endregion
    }
}