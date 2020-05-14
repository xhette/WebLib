using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.DataLayer;
using WebLib.Models;
using WebLib.Models.LibrarianPages;
using WebLib.Models.ProviderPages;
using WebLib.Models.ReaderPages;
using WebMatrix.WebData;

namespace WebLib.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
        private SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

        private LibContext context;

        public AdminController()
        {
            context = new LibContext();
        }

        public ActionResult Index()
        {
            return View();
        }

		#region Authors
		public ActionResult Authors()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddAuthor()
        {
            AuthorModel model = new AuthorModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddAuthor(AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                AuthorBs bs = new AuthorBs();
                var result = bs.Add((AuthorDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Автор успешно добавлен";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при добавлении автора";
                }

                return RedirectToAction("Authors", "Admin");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditAuthor(int id)
        {
            AuthorBs bs = new AuthorBs();

            AuthorModel model = (AuthorModel)bs.GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAuthor(AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                AuthorBs bs = new AuthorBs();
                var result = bs.Update((AuthorDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Автор успешно изменен";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при изменении автора";
                }

                return RedirectToAction("Authors", "Admin");
            }

            return View(model);
        }

        public ActionResult DeleteAuthor(int id)
        {
            AuthorBs bs = new AuthorBs();
            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Автор успешно добавлен";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Ошибка при добавлении автора";
            }

            return RedirectToAction("Authors", "Admin");
        }
		#endregion

		#region Books
		public ActionResult Books()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            BookBs bs = new BookBs();
            AuthorBs authorBs = new AuthorBs();

            BookEditModel model = new BookEditModel();
            model.Book = new BookModel();
            model.Authors = authorBs.GetList().Select(c => (AuthorModel)c).ToList();
            LibraryBs libraryBs = new LibraryBs();
            model.Libraries = libraryBs.GetList().Select(c => (LibraryModel)c).ToList();
            DepartmentBs departmentBs = new DepartmentBs();

            var lib = model.Libraries.FirstOrDefault();

            if (lib != null)
            {
                model.SelectedLib = lib.Id;
                var departments = departmentBs.GetList().Where(c => c.LibraryId == model.SelectedLib).ToList();

                if (departments != null)
                {
                    model.Departments = departments.Select(c => (DepartmentModel)c).ToList();
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddBook(BookEditModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Book.DepartmentId == 0)
                    model.Book.DepartmentId = null;

                BookBs bs = new BookBs();
                var result = bs.Add((BookDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";

                    return RedirectToAction("Books", "Admin");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            AuthorBs authorBs = new AuthorBs();
            model.Authors = authorBs.GetList().Select(c => (AuthorModel)c).ToList();
            DepartmentBs dbs = new DepartmentBs();
            model.Departments = dbs.GetList().Where(c => c.LibraryId == model.SelectedLib).Select(c => (DepartmentModel)c).ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult EditBook(int id)
        {
            BookBs bs = new BookBs();
            AuthorBs authorBs = new AuthorBs();

            BookEditModel model = new BookEditModel();
            model.Book = (BookModel)bs.GetById(id);
            model.Authors = authorBs.GetList().Select(c => (AuthorModel)c).ToList();
            LibraryBs libraryBs = new LibraryBs();
            model.Libraries = libraryBs.GetList().Select(c => (LibraryModel)c).ToList();

            if (model.Book.DepartmentId.HasValue && model.Book.DepartmentId.Value > 0)
            {
                DepartmentBs departmentBs = new DepartmentBs();
                if (model.SelectedLib == 0)
                {
                    model.SelectedLib = departmentBs.GetById(model.Book.DepartmentId.Value).LibraryId;
                }
                var departments = departmentBs.GetList().Where(c => c.LibraryId == model.SelectedLib).ToList();

                if (departments != null)
                {
                    model.Departments = departments.Select(c => (DepartmentModel)c).ToList();
                }
            }
            else
            {
                DepartmentBs departmentBs = new DepartmentBs();

                var lib = model.Libraries.FirstOrDefault();

                if (lib != null)
                {
                    model.SelectedLib = lib.Id;
                    var departments = departmentBs.GetList().Where(c => c.LibraryId == model.SelectedLib).ToList();

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

                    return RedirectToAction("Books", "Admin");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            AuthorBs authorBs = new AuthorBs();
            model.Authors = authorBs.GetList().Select(c => (AuthorModel)c).ToList();
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
            return RedirectToAction("Books", "Admin");
        }
		#endregion

		#region Cities
		public ActionResult Cities()
        {
            TempData["IsAdminView"] = true;
            return View();
        }


        [HttpGet]
        public ActionResult AddCity()
        {
            CityModel model = new CityModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddCity(CityModel model)
        {
            if (ModelState.IsValid)
            {
                CityBs bs = new CityBs();
                var result = bs.Add((CityDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Операция завершена упешно";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при выполнении операции";
                }

                return RedirectToAction("Cities", "Admin");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditCity(int id)
        {
            CityBs bs = new CityBs();

            CityModel model = (CityModel)bs.GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCity(CityModel model)
        {
            if (ModelState.IsValid)
            {
                CityBs bs = new CityBs();
                var result = bs.Update((CityDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Операция завершена упешно";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при выполнении операции";
                }

                return RedirectToAction("Cities", "Admin");
            }

            return View(model);
        }

        public ActionResult DeleteCity(int id)
        {
            CityBs bs = new CityBs();
            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Операция завершена упешно";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Ошибка при выполнении операции";
            }

            return RedirectToAction("Cities", "Admin");
        }
		#endregion

		#region Departments
		public ActionResult Departments()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            LibraryBs libBs = new LibraryBs();

            EditDepartmentModel model = new EditDepartmentModel();
            model.Libraries = libBs.GetList().Select(c => (LibraryModel)c).ToList();

            if(model.Libraries != null && model.Libraries.Count > 0)
            {
                model.LibraryId = model.Libraries.FirstOrDefault().Id;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddDepartment(EditDepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                DepartmentBs bs = new DepartmentBs();

                var result = bs.Add((DepartmentDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Операция завершена упешно";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при выполнении операции";
                }

                return RedirectToAction("Departments", "Admin");
            }

            LibraryBs libBs = new LibraryBs();
            model.Libraries = libBs.GetList().Select(c => (LibraryModel)c).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult EditDepartment(int id)
        {
            LibraryBs libBs = new LibraryBs();
            DepartmentBs bs = new DepartmentBs();

            EditDepartmentModel model = (EditDepartmentModel)bs.GetById(id);
            model.Libraries = libBs.GetList().Select(c => (LibraryModel)c).ToList();

            if (model.Libraries != null && model.Libraries.Count > 0)
            {
                model.LibraryId = model.Libraries.FirstOrDefault().Id;
            }

            return View(model);
        }

        public ActionResult DeleteDepartment(int id)
        {
            DepartmentBs bs = new DepartmentBs();

            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Операция завершена упешно";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Ошибка при выполнении операции";
            }

            return RedirectToAction("Departments", "Admin");
        }
        [HttpPost]
        public ActionResult EditDepartment(EditDepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                DepartmentBs bs = new DepartmentBs();

                var result = bs.Update((DepartmentDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Операция завершена упешно";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Ошибка при выполнении операции";
                }

                return RedirectToAction("Departments", "Admin");
            }

            LibraryBs libBs = new LibraryBs();
            model.Libraries = libBs.GetList().Select(c => (LibraryModel)c).ToList();
            return View(model);
        }
		#endregion

		#region Issues
		public ActionResult Issues()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        public ActionResult DeleteIssue(int id)
        {
            IssueBs bs = new IssueBs();

            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Выдача успешно удалена";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при удалении выдачи";
            }

            return RedirectToAction("Issues", "Admin");
        }
		#endregion

		#region Librarians
		public ActionResult Librarians()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddLibrarian()
        {
            LibrarianEditModel model = new LibrarianEditModel();

            LibraryBs libbs = new LibraryBs();
            model.Libraries = libbs.GetList().Select(c => (LibraryModel)c).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLibrarian(LibrarianEditModel model)
        {
            if (ModelState.IsValid)
            {
                LibrarianBs bs = new LibrarianBs();

                var result = bs.Add((LibrarianDataDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Произошла ошибка при обновлении данных";
                }

                return RedirectToAction("Librarians", "Admin");
            }

            LibraryBs libbs = new LibraryBs();
            model.Libraries = libbs.GetList().Select(c => (LibraryModel)c).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult EditLibrarian(int id)
        {
            LibrarianBs librarianBs = new LibrarianBs();

            LibrarianEditModel model = (LibrarianEditModel)librarianBs.GetById(id);

            LibraryBs libbs = new LibraryBs();
            model.Libraries = libbs.GetList().Select(c => (LibraryModel)c).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditLibrarian(LibrarianEditModel model)
        {
            if (ModelState.IsValid)
            {
                LibrarianBs bs = new LibrarianBs();

                var result = bs.Update((LibrarianDataDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Произошла ошибка при обновлении данных";
                }

                return RedirectToAction("Librarians", "Admin");
            }

            LibraryBs libbs = new LibraryBs();
            model.Libraries = libbs.GetList().Select(c => (LibraryModel)c).ToList();

            return View(model);
        }

        public ActionResult DeleteLibrarian(int id)
        {
            LibrarianBs bs = new LibrarianBs();

            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Данные успешно удалены";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при удалении данных";
            }

            return RedirectToAction("Librarians", "Admin");
        }
        #endregion

        #region Libraries
        public ActionResult Libraries()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddLibrary()
        {
            EditLibraryModel model = new EditLibraryModel();

            CityBs bs = new CityBs();
            model.Cities = bs.GetList().Select(c => (CityModel)c).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLibrary(EditLibraryModel model)
        {
            if (ModelState.IsValid)
            {
                LibraryBs bs = new LibraryBs();
                var result = bs.Add((LibraryDTO)model);

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

                return RedirectToAction("Libraries", "Admin");
            }

            CityBs citybs = new CityBs();
            model.Cities = citybs.GetList().Select(c => (CityModel)c).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult EditLibrary(int id)
        {
            LibraryBs libbs = new LibraryBs();
            EditLibraryModel model = (EditLibraryModel)libbs.GetById(id);

            CityBs bs = new CityBs();
            model.Cities = bs.GetList().Select(c => (CityModel)c).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditLibrary(EditLibraryModel model)
        {
            if (ModelState.IsValid)
            {
                LibraryBs bs = new LibraryBs();
                var result = bs.Update((LibraryDTO)model);

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

                return RedirectToAction("Libraries", "Admin");
            }

            CityBs citybs = new CityBs();
            model.Cities = citybs.GetList().Select(c => (CityModel)c).ToList();
            return View(model);
        }

        public ActionResult DeleteLibrary(int id)
        {
            LibraryBs bs = new LibraryBs();
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

            return RedirectToAction("Libraries", "Admin");
        }
        #endregion

        #region Providers
        public ActionResult Providers()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddProvider()
        {
            ProviderModel model = new ProviderModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddProvider(ProviderModel model)
        {
            if (ModelState.IsValid)
            {
                ProviderBs bs = new ProviderBs();

                var result = bs.Add((ProviderDataDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Произошла ошибка при обновлении данных";
                }

                return RedirectToAction("Providers", "Admin");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditProvider(int id)
        {
            ProviderBs bs = new ProviderBs();

            ProviderModel model = (ProviderModel)bs.GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProvider(ProviderModel model)
        {
            if (ModelState.IsValid)
            {
                ProviderBs bs = new ProviderBs();

                var result = bs.Update((ProviderDataDTO)model);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Данные успешно обновлены";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Произошла ошибка при обновлении данных";
                }

                return RedirectToAction("Providers", "Admin");
            }

            return View(model);
        }

        public ActionResult DeleteProvider(int id)
        {
            ProviderBs bs = new ProviderBs();

            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Данные успешно удалены";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при удалении данных";
            }

            return RedirectToAction("Providers", "Admin");
        }
        #endregion

        #region Readers
        public ActionResult Readers()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        public ActionResult DeleteReader (int id)
        {
            ReaderBs bs = new ReaderBs();
            var reader = bs.GetById(id);

            if (reader.UserId != null)
            {
                var user = context.Users.FirstOrDefault(c => c.Id == reader.UserId);
                if (user != null) 
                {
                    string userName = user.Name;
                    Utils.CreateAccounts.DeleteUserIfExist(userName);
                }
            }

            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Читатель успешно удален";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при удалении читателя";
            }

            return RedirectToAction("Readers", "Admin");
        }
		#endregion

		#region Shops
		public ActionResult Shops()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddShop()
        {
            ShopModel model = new ShopModel();

            return View(model);
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

                    return RedirectToAction("Shops", "Admin");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return View( model);
        }

        [HttpGet]
        public ActionResult EditShop(int id)
        {
            ShopBs bs = new ShopBs();
            ShopModel model = (ShopModel)bs.GetById(id);

            return View(model);
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

                    return RedirectToAction("Shops", "Admin");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return View(model);
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
            return RedirectToAction("Shops", "Admin");
        }
        #endregion

        #region Supplies
        public ActionResult Supplies()
        {
            TempData["IsAdminView"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult AddSupply()
        {
            ShopBs bs = new ShopBs();
            SupplyAddModel model = new SupplyAddModel();
            model.Shops = bs.GetList().Select(c => (ShopModel)c).ToList();
            model.Supply.ShopId = model.Shops.FirstOrDefault().Id;

            return View(model);
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

                    return RedirectToAction("Supplies", "Admin");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditSupply(int id)
        {
            ShopBs bs = new ShopBs();
            SupplyBs supplyBs = new SupplyBs();
            SupplyAddModel model = (SupplyAddModel)supplyBs.GetById(id);
            model.Shops = bs.GetList().Select(c => (ShopModel)c).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditSupply(SupplyAddModel model)
        {
            if (ModelState.IsValid)
            {
                SupplyBs bs = new SupplyBs();
                var result = bs.Update((SupplyDTO)model.Supply);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Поставка успешно изменена";

                    return RedirectToAction("Supplies", "Admin");
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = result.Message;
                }
            }

            return View(model);
        }

        public ActionResult DeleteSupply(int id)
        {
            SupplyBs bs = new SupplyBs();
            var result = bs.Delete(id);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Поставка успешно удалена";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = result.Message;
            }

            return RedirectToAction("Supplies", "Admin");
        }
        #endregion

        #region partial lists
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

        public ActionResult BooksList (string symbols = "")
        {
            BookBs book = new BookBs();
            List<BookModel> model = book.GetList().Where(c => c.Title.Contains(symbols)).Select(c => (BookModel)c).ToList();

            return PartialView("~/Views/Admin/_BooksList.cshtml", model);
        }

        public ActionResult CitiesList (string symbols = "")
        {
            CityBs city = new CityBs();
            List<CityModel> model = city.GetList().Where(c => c.Name.Contains(symbols)).Select(c => (CityModel)c).ToList();

            return PartialView("~/Views/Admin/_CitiesList.cshtml", model);
        }

        public ActionResult DepartmentsList(string symbols = "")
        {
            DepartmentBs city = new DepartmentBs();
            List<DepartmentModel> model = city.GetList().Where(c => c.Name.Contains(symbols)).Select(c => (DepartmentModel)c).ToList();

            return PartialView("~/Views/Admin/_DepartmentsList.cshtml", model);
        }

        public ActionResult IssuesList()
        {
            IssueBs issue = new IssueBs();
            List<IssueModel> model = issue.GetList().Select(c => (IssueModel)c).ToList();

            return PartialView("~/Views/Admin/_IssuesList.cshtml", model);
        }

        public ActionResult LibrariansList (string symbols = "")
        {
            LibrarianBs librarian = new LibrarianBs();
            List<LibrarianModel> model = librarian.GetList().Where(
                c => c.Name.Contains(symbols)
                || c.Surname.Contains(symbols)
                || c.Patronymic.Contains(symbols))
                .Select(c => (LibrarianModel)c).ToList();

            return PartialView("~/Views/Admin/_LibrariansList.cshtml", model);
        }

        public ActionResult LibrariesList(string symbols = "")
        {
            LibraryBs library = new LibraryBs();
            List<LibraryModel> model = library.GetList().Where(c => c.Name.Contains(symbols)).Select(c => (LibraryModel)c).ToList();

            return PartialView("~/Views/Admin/_LibrariesList.cshtml", model);
        }

        public ActionResult ProvidersList(string symbols = "")
        {
            ProviderBs provider = new ProviderBs();
            List<ProviderModel> model = provider.GetList().Where(
                c => c.Name.Contains(symbols)
                || c.Surname.Contains(symbols)
                || c.Patronymic.Contains(symbols))
                .Select(c => (ProviderModel)c).ToList();

            return PartialView("~/Views/Admin/_ProvidersList.cshtml", model);
        }

        public ActionResult ReadersList(string symbols = "")
        {
            ReaderBs reader = new ReaderBs();
            List<ReaderDataModel> model = reader.GetList().Where(
                c => c.Name.Contains(symbols)
                || c.Surname.Contains(symbols)
                || c.Patronymic.Contains(symbols))
                .Select(c => (ReaderDataModel)c).ToList();

            return PartialView("~/Views/Admin/_ReadersList.cshtml", model);
        }

        public ActionResult ShopsList(string symbols = "")
        {
            ShopBs shop = new ShopBs();
            List<ShopModel> model = shop.GetList().Where(c => c.Name.Contains(symbols)).Select(c => (ShopModel)c).ToList();

            return PartialView("~/Views/Admin/_ShopsList.cshtml", model);
        }

        public ActionResult SuppliesList()
        {
            SupplyBs supply = new SupplyBs();
            List<SupplyModel> model = supply.GetList().Select(c => (SupplyModel)c).ToList();

            return PartialView("~/Views/Admin/_SuppliesList.cshtml", model);
        }
		#endregion


	}
}