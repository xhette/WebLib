﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.DataLayer;
using WebLib.Enums;
using WebLib.Models;
using WebLib.Models.LibrarianPages;
using WebLib.Models.ReaderPages;
using WebMatrix.WebData;

namespace WebLib.Controllers
{

    [Authorize(Roles = "librarian")]
    public class LibrarianPageController : Controller
    {
        private SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
        private SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

        private LibrarianPage librarianContext;
        private LibContext context;

        public LibrarianPageController()
        {
            context = new LibContext();
            librarianContext = new LibrarianPage(context);
        }

        public ActionResult Index(int page = 1)
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
            }

            ViewBag.SearchingType = page;

            return View();
        }

        public ActionResult SidebarPartial()
        {
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;

                LibrarianModel model = (LibrarianModel)librarianContext.LibrarianData(librarianId);

                return PartialView("~/Views/LibrarianPage/_LibrarianInfo.cshtml", model);
            }
            return null;
        }

        #region BookSearch
        public ActionResult BooksByTitle()
        {
            List<BookViewModel> model = new List<BookViewModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;

                model = librarianContext.Books(libId).Select(c => (BookViewModel)c).ToList();
                model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            }
            return PartialView("~/Views/LibrarianPage/_BooksByTitle.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult BookSearch(string symbols)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;

            if (!String.IsNullOrEmpty(symbols))
            {
                model = librarianContext.Books(libId).Where(c => c.Title.Contains(symbols)).Select(c => (BookViewModel)c).ToList();
            }
            else
            {
                model = librarianContext.Books(libId).Select(c => (BookViewModel)c).ToList();
            }

            ViewBag.TableHead = true;
            model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            }
            return PartialView("~/Views/LibrarianPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByAuthor()
        {
            List<AuthorModel> model;
            librarianContext = new LibrarianPage(context);
            model = librarianContext.Authors().Select(c => (AuthorModel)c).ToList();
            return PartialView("~/Views/LibrarianPage/_BooksByAuthor.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorSearch(string symbols)
        {
            List<AuthorModel> model;
            model = librarianContext.Authors().Where(c => c.Surname.Contains(symbols) || c.Name.Contains(symbols) || c.Patronymic.Contains(symbols))
                .Select(c => (AuthorModel)c).ToList();

            return PartialView("~/Views/LibrarianPage/_AuthorSearch.cshtml", model);

        }
        public ActionResult BookSearchByAuthor(int authorId)
        {
            List<BookViewModel> model = new List<BookViewModel>();
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                if (authorId > 0)
                {
                    model = librarianContext.Books(libId).Where(c => c.AuthorId == authorId).Select(c => (BookViewModel)c).ToList();
                }

                ViewBag.TableHead = false;
                model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            }
            return PartialView("~/Views/LibrarianPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByDepartment()
        {

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);
            DepartmentListModel model = new DepartmentListModel();
            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
               model = (DepartmentListModel)librarianContext.Departments(libId);
            }

            return PartialView("~/Views/LibrarianPage/_BooksByDepartment.cshtml", model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentSearch(string symbols)
        {
            DepartmentListModel model = new DepartmentListModel();
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                model = (DepartmentListModel)librarianContext.Departments(symbols, libId);
            }
            return PartialView("~/Views/LibrarianPage/_DepartmentSearch.cshtml", model);
        }

        public ActionResult BookSearchByDepartment(int deptId)
        {
            List<BookViewModel> model = new List<BookViewModel>();
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                if (deptId > 0)
                {
                    model = librarianContext.Books(libId).Where(c => c.DepartmentId == deptId).Select(c => (BookViewModel)c).ToList();
                }
                ViewBag.TableHead = false;
                model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            }
            return PartialView("~/Views/LibrarianPage/_BookSearch.cshtml", model);
        }

		#endregion

		#region Issues
		public ActionResult Issues(int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        public ActionResult AllIssues()
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);
            List<IssueDetailedModel> model = new List<IssueDetailedModel>();

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                model = librarianContext.IssuesList(libId).Select(c => (IssueDetailedModel)c).ToList();
            }
            return PartialView("~/Views/LibrarianPage/_IssuesSearch.cshtml", model);
        }

        public ActionResult SpoiledIssues()
        {
            List<IssueDetailedModel> model = new List<IssueDetailedModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                DateTime date = DateTime.Today.AddDays(-14);
                model = librarianContext.IssuesList(libId)
                    .Where(c => c.Issue.IssueDate < date && c.Issue.ReturnDate == null)
                    .Select(c => (IssueDetailedModel)c).ToList();
            }
            return PartialView("~/Views/LibrarianPage/_IssuesSearch.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditIssue(int id = 0)
        {
            EditIssueModel model = new EditIssueModel();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                var issue = librarianContext.IssuesList(libId).Where(c => c.Issue.Id == id).FirstOrDefault();
                model = (EditIssueModel)issue;
                model.Authors = librarianContext.Authors().Select(c => (AuthorModel)c).ToList();
                model.Readers = librarianContext.AbonentList(libId).Where(c => c.Status == 3).Select(c => new ReaderDataModel
                {
                    Id = c.Reader.Id,
                    Surname = c.Reader.Surname,
                    Name = c.Reader.Name,
                    Patronymic = c.Reader.Patronymic
                }).ToList();

                model.Books = librarianContext.Books(libId).Where(c => c.AuthorId == model.SelectedAuthor).Select(c => (BookModel)c).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditIssue(EditIssueModel model)
        {
            if (ModelState.IsValid)
            {
                IssueDTO issue = (IssueDTO)model;
                var result = librarianContext.UpdateIssue(issue);

                if (result)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Новые данные успешно сохранены";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Произошла ошибка при обновлении данных";
                }

                return RedirectToAction("Issues", "LibrarianPage");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult AddIssue(int id)
        {
            AddIssueModel model = new AddIssueModel();
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                model.BookId = id;
                model.Readers = librarianContext.AbonentList(libId).Where(c => c.Status == 3).Select(c => new ReaderDataModel
                {
                    Id = c.Reader.Id,
                    Surname = c.Reader.Surname,
                    Name = c.Reader.Name,
                    Patronymic = c.Reader.Patronymic
                }).ToList();
            }
            return PartialView("~/Views/LibrarianPage/_AddIssue.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddIssue(AddIssueModel model)
        {
            if (ModelState.IsValid)
            {
                var result = librarianContext.AddIssue(model.BookId, model.ReaderId);

                if (result)
                {
                    TempData["OperationStatus"] = true;
                    TempData["OpearionMessage"] = "Книга успешно выдана";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "Произошла ошибка при выдаче книги";
                }

                return RedirectToAction("Issues", "LibrarianPage");
            }
            else
            {
                return PartialView("~/Views/LibrarianPage/_AddIssue.cshtml", model);
            }
        }

        public ActionResult DeleteIssue (int id)
        {
            var result = librarianContext.DeleteIssue(id);

            if (result)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Выдача успешно удалена";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при удалении выдачи";
            }

            return RedirectToAction("Issues", "LibrarianPage");
        }
		#endregion

		#region AbonentClaims
        public ActionResult Claims()
        {
            List<AbonentModel> model = new List<AbonentModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;
                model = librarianContext.AbonentList(libId).Select(c => (AbonentModel)c).ToList();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeStatus (int libId, int readerId, AbonentStatusEnum status)
        {
            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                StatusChangeModel model = new StatusChangeModel
                {
                    LibraryId = libId,
                    ReaderId = readerId,
                    Status = status
                };

                IEnumerable<AbonentStatusEnum> values = Enum.GetValues(typeof(AbonentStatusEnum)).Cast<AbonentStatusEnum>();
                model.StatusList = values.Select(value => new SelectListItem
                {
                    Text = Utils.Enums.GetDescription(value),
                    Value = value.ToString()
                });
                return PartialView("~/Views/LibrarianPage/_ChangeAbonentStatus.cshtml", model);
            }

            return RedirectToAction("Index", "LibrarianPage");
        }

        [HttpPost]
        public ActionResult ChangeStatus (StatusChangeModel model)
        {
            var result = librarianContext.ChangeStatusAbonent(model.LibraryId, model.ReaderId, (int)model.Status);

            if (result)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Статус успешно изменен";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при смене статуса";
            }

            return RedirectToAction("Claims", "LibrarianPage");
        }

        public ActionResult DeleteAbonent(int libId, int readerId)
        {
            var result = librarianContext.DeleteAbonent(libId, readerId);

            if (result)
            {
                TempData["OperationStatus"] = true;
                TempData["OpearionMessage"] = "Читатель удален";
            }
            else
            {
                TempData["OperationStatus"] = false;
                TempData["OpearionMessage"] = "Произошла ошибка при удалении читателя";
            }

            return RedirectToAction("Claims", "LibrarianPage");
        }
		#endregion

		#region Readers
        public ActionResult Readers(int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        public ActionResult AllReaders()
        {
            List<ReaderDataModel> model = new List<ReaderDataModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;

                model = librarianContext.AbonentList(libId).Where(c => c.Status == 3).Select(c => (ReaderDataModel)c).ToList();
            }

            return PartialView("~/Views/LibrarianPage/_AllReaders.cshtml", model); ;
        }

        public ActionResult Deptors()
        {

            List<ReaderDataModel> model = new List<ReaderDataModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;

                model = librarianContext.AbonentListSpoiled(libId).Where(c => c.Status == 3).Select(c => (ReaderDataModel)c).ToList();
            }
            return PartialView("~/Views/LibrarianPage/_Deptors.cshtml", model); ;
        }

        public ActionResult ReaderData(int readerId)
        {
            ReaderDataModel model = (ReaderDataModel)librarianContext.ReaderInfo(readerId);

            return PartialView("~/Views/LibrarianPage/_ReaderInfo.cshtml", model); ;
        }

        #endregion

        #region DropdownPartials

        [HttpGet]
        public ActionResult BookListByAuthor (int id = 0)
        {
            List<BookModel> model = new List<BookModel>();

            if (!WebSecurity.IsAuthenticated) RedirectToAction("Index", "Login");
            int userId = WebSecurity.GetUserId(User.Identity.Name);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                int librarianId = librarian.Id;
                int libId = librarian.Library;

                 model = librarianContext.Books(libId).Select(c => new BookModel
                {
                    Id = c.BookId,
                    AuthorId = c.AuthorId,
                    Title = c.Title,
                    DepartmentId = c.DepartmentId
                }).ToList();

                if (id != 0)
                {
                    model = model.Where(c => c.AuthorId == id).ToList();
                }
            }
            return PartialView("~/Views/Shared/_BookDropdown.cshtml", model);
        }
        #endregion
    }
}