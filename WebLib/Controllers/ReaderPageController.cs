using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.DataLayer;
using WebLib.Models;
using WebLib.Models.ReaderPages;
using WebMatrix.WebData;

namespace WebLib.Controllers
{

    [Authorize(Roles = "reader")]
    public class ReaderPageController : Controller
    {
        private SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
        private SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

        private ReaderPage readerContext;
        private LibContext context;

        public ReaderPageController ()
        {
            context = new LibContext();
            readerContext = new ReaderPage(context);
        }


        public ActionResult Index ()
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);

            int readerId;

            if (reader != null)
            {
                readerId = reader.Id;
            }

            ReaderPageModel model = new ReaderPageModel();
            model.Reader = (ReaderDataModel)readerContext.ReaderData(userId);
            model.Libraries = readerContext.AbonentList(model.Reader.Id).Select(c => (LibraryShortModel)c).ToList();
            var dbIssues = readerContext.ReaderIssuesList(model.Reader.Id).ToList();
            if (dbIssues.Count > 5)
                dbIssues.Skip(Math.Max(0, dbIssues.Count() - 5));

            model.Issues = dbIssues.Select(c => (ReaderIssueModel)c).ToList();
            return View(model);
        }

        public ActionResult Libraries ()
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            List<LibraryReaderModel> model = new List<LibraryReaderModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);

            int readerId = 0;

            if (reader != null)
            {
                readerId = reader.Id;
            }

            if (readerId != 0)
            {

                model = readerContext.LibraryInfoAbonent(readerId).Select(c => (LibraryReaderModel)c).ToList();
                cities = readerContext.CityList().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                ViewBag.CitySelectList = cities;

                return View(model);
            }
            else return View();
        }

        public ActionResult LibraryInfo (int libId)
        {
            LibraryModel model;

            model = (LibraryModel)readerContext.LibraryInfo(libId);

            return PartialView("~/Views/ReaderPage/_LibraryInfo.cshtml", model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult LibrariesByCity (int? cityId)
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);

            List<LibraryReaderModel> model = new List<LibraryReaderModel>();
            int readerId = context.Readers.FirstOrDefault(c => c.UserId == userId).Id;

            if (cityId.HasValue)
            {
                model = readerContext.LibraryInfoAbonent(readerId).Where(c => c.City.Id == cityId.Value).Select(c => (LibraryReaderModel)c).ToList(); ;
            }
            else
            {
                model = readerContext.LibraryInfoAbonent(readerId).Select(c => (LibraryReaderModel)c).ToList();
            }
            return PartialView("~/Views/ReaderPage/_LibrariesByCity.cshtml", model);
        }

        public ActionResult Issues ()
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Login", "Home");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);

            int readerId;

            if (reader != null)
            {
                readerId = reader.Id;

                List<ReaderIssueModel> model;
                readerContext = new ReaderPage(context);
                var dbIssues = readerContext.ReaderIssuesList(readerId).ToList();

                model = dbIssues.Select(c => (ReaderIssueModel)c).ToList();
                return View(model);
            }
            else return View();
        }

        public ActionResult Books (int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        #region BookSearch
        public ActionResult BooksByTitle ()
        {
            List<BookViewModel> model;

            model = readerContext.Books().Select(c => (BookViewModel)c).ToList();
            return PartialView("~/Views/ReaderPage/_BooksByTitle.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult BookSearch (string symbols)
        {
            List<BookViewModel> model;

            if (!String.IsNullOrEmpty(symbols))
            {
                model = readerContext.Books().Where(c => c.Title.Contains(symbols)).Select(c => (BookViewModel)c).ToList();
            }
            else
            {
                model = readerContext.Books().Select(c => (BookViewModel)c).ToList();
            }

            ViewBag.TableHead = true;
            return PartialView("~/Views/ReaderPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByAuthor ()
        {
            List<AuthorModel> model;
            readerContext = new ReaderPage(context);
            model = readerContext.Authors().Select(c => (AuthorModel)c).ToList();
            return PartialView("~/Views/ReaderPage/_BooksByAuthor.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorSearch (string symbols)
        {
            List<AuthorModel> model;
            model = readerContext.Authors().Where(c => c.Surname.Contains(symbols) || c.Name.Contains(symbols) || c.Patronymic.Contains(symbols))
                .Select(c => (AuthorModel)c).ToList();

            return PartialView("~/Views/ReaderPage/_AuthorSearch.cshtml", model);

        }
        public ActionResult BookSearchByAuthor (int authorId)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            if (authorId > 0)
            {
                model = readerContext.Books().Where(c => c.AuthorId == authorId).Select(c => (BookViewModel)c).ToList();
            }

            ViewBag.TableHead = false;
            return PartialView("~/Views/ReaderPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByDepartment ()
        {
            List<DepartmentListModel> model = new List<DepartmentListModel>();
            model = readerContext.Departments().Select(c => (DepartmentListModel)c).ToList();

            return PartialView("~/Views/ReaderPage/_BooksByDepartment.cshtml", model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentSearch (string symbols)
        {
            List<DepartmentListModel> model = new List<DepartmentListModel>();
            model = readerContext.Departments(symbols).Select(c => (DepartmentListModel)c).ToList();
            return PartialView("~/Views/ReaderPage/_DepartmentSearch.cshtml", model);
        }

        public ActionResult BookSearchByDepartment (int deptId)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            if (deptId > 0)
            {
                model = readerContext.Books().Where(c => c.DepartmentId == deptId).Select(c => (BookViewModel)c).ToList();
            }
            ViewBag.TableHead = false;
            return PartialView("~/Views/ReaderPage/_BookSearch.cshtml", model);
        }

        #endregion

        public ActionResult AddAbonent (int libId)
        {
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);

            TempData["OperationStatus"] = false;
            int readerId = context.Readers.FirstOrDefault(c => c.UserId == userId).Id;

            var result = readerContext.AddAbonentClaim(readerId, libId);

            if (result.Code == BusinessLayer.OperationStatusEnum.Success)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = result.Message;
            }
            else
            {
                TempData["OpearionMessage"] = result.Message;
            }

            return RedirectToAction("Libraries", "ReaderPage");
        }

        public ActionResult Settings (int page = 1)
        {
            ViewBag.Page = page;

            return View();
        }

        [HttpGet]
        public ActionResult ChangeInfo ()
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            ReaderDataModel model = (ReaderDataModel)readerContext.ReaderData(userId);

            return PartialView("~/Views/ReaderPage/_ChangeInfo.cshtml", model);
        }

        [HttpPost]
        public ActionResult ChangeInfo (ReaderDataModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["OperationStatus"] = false;

                ReaderDataDTO reader = (ReaderDataDTO)model;
                var result = readerContext.UpdateReader(reader);

                if (result.Code == BusinessLayer.OperationStatusEnum.Success)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = result.Message;
                }
                else
                {
                    TempData["OpearionMessage"] = result.Message;
                }
                return RedirectToAction("Settings", "ReaderPage");
            }
            else
            {

                return PartialView("~/Views/ReaderPage/_ChangeInfo.cshtml", model);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword ()
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var reader = context.Readers.FirstOrDefault(c => c.UserId == userId);
            ChangePasswordModel model = new ChangePasswordModel
            {
                UserId = userId
            };

            return PartialView("~/Views/ReaderPage/_ChangePassword.cshtml", model);
        }

        [HttpPost]
        public ActionResult ChangePassword (ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["OperationStatus"] = false;

                string userName = User.Identity.Name;
                if (WebSecurity.ChangePassword(userName, model.OldPassword, model.NewPassword))
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Пароль успешно изменен.";

                    return RedirectToAction("Settings", "ReaderPage", new { page = 2 });
                }
                else
                {
                    TempData["OpearionMessage"] = "Произошла ошибка при смене пароля. Попробуйте еще раз.";
                }
            }
            return PartialView("~/Views/ReaderPage/_ChangePassword.cshtml", model);
        }
    }
}