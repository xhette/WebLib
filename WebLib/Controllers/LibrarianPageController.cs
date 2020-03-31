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
using WebLib.Models.LibrarianPages;
using WebLib.Models.ReaderPages;

namespace WebLib.Controllers
{
    public class LibrarianPageController : Controller
    {
        private LibrarianPage librarianContext;
        private LibContext context;
        private int userId;
        private int librarianId;
        private int libId;

        public LibrarianPageController()
        {
            //userId = WebSecurity.GetUserId(User.Identity.Name);
            //if (userId == 0) 
            userId = 100;

            context = new LibContext();
            librarianContext = new LibrarianPage(context);

            var librarian = context.Librarians.FirstOrDefault(c => c.UserId == userId);

            if (librarian != null)
            {
                librarianId = librarian.Id;
                libId = librarian.Library;
            }
        }

        public ActionResult Index(int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        public ActionResult SidebarPartial()
        {
            LibrarianModel model = (LibrarianModel)librarianContext.LibrarianData(librarianId);

            return PartialView("~/Views/LibrarianPage/_LibrarianInfo.cshtml", model);
        }

        #region BookSearch
        public ActionResult BooksByTitle()
        {
            List<BookViewModel> model;

            model = librarianContext.Books(libId).Select(c => (BookViewModel)c).ToList();
            model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            return PartialView("~/Views/LibrarianPage/_BooksByTitle.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult BookSearch(string symbols)
        {
            List<BookViewModel> model;

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

            if (authorId > 0)
            {
                model = librarianContext.Books(libId).Where(c => c.AuthorId == authorId).Select(c => (BookViewModel)c).ToList();
            }

            ViewBag.TableHead = false;
            model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            return PartialView("~/Views/LibrarianPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByDepartment()
        {
            DepartmentListModel model = (DepartmentListModel)librarianContext.Departments(libId);

            return PartialView("~/Views/LibrarianPage/_BooksByDepartment.cshtml", model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentSearch(string symbols)
        {
            DepartmentListModel model = (DepartmentListModel)librarianContext.Departments(symbols, libId);
            return PartialView("~/Views/LibrarianPage/_DepartmentSearch.cshtml", model);
        }

        public ActionResult BookSearchByDepartment(int deptId)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            if (deptId > 0)
            {
                model = librarianContext.Books(libId).Where(c => c.DepartmentId == deptId).Select(c => (BookViewModel)c).ToList();
            }
            ViewBag.TableHead = false;
            model.ForEach(c => c.GiveOut = librarianContext.GiveOut(c.BookId));
            return PartialView("~/Views/LibrarianPage/_BookSearch.cshtml", model);
        }

        #endregion

        public ActionResult Issues(int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        public ActionResult AllIssues()
        {
            List<IssueDetailedModel> model = librarianContext.IssuesList(libId).Select(c => (IssueDetailedModel)c).ToList();

            return PartialView("~/Views/LibrarianPage/_IssuesSearch.cshtml", model);
        }

        public ActionResult SpoiledIssues()
        {
            DateTime date = DateTime.Today.AddDays(-14);
            List<IssueDetailedModel> model = librarianContext.IssuesList(libId)
                .Where(c => c.Issue.IssueDate < date && c.Issue.ReturnDate == null)
                .Select(c => (IssueDetailedModel)c).ToList();

            return PartialView("~/Views/LibrarianPage/_IssuesSearch.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditIssue(int id)
        {
            var issue = librarianContext.IssuesList(libId).Where(c => c.Issue.Id == id);
            EditIssueModel model = (EditIssueModel)issue;
            model.Authors = librarianContext.Authors().Select(c => (AuthorModel)c).ToList();
            model.Readers = librarianContext.AbonentList(libId).Select(c => new ReaderDataModel
            {
                Id = c.Reader.Id,
                Surname = c.Reader.Surname,
                Name = c.Reader.Name,
                Patronymic = c.Reader.Patronymic
            }).ToList();

            model.Books = librarianContext.Books(libId).Where(c => c.AuthorId == model.SelectedAuthor).Select(c => (BookModel)c).ToList();

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
                    TempData["OpearionMessage"] = "";
                }
                else
                {
                    TempData["OperationStatus"] = false;
                    TempData["OpearionMessage"] = "";
                }

                return RedirectToAction("Issues", "LibrarianPage");
            }
            else
            {
                return View(model);
            }
        }

        #region DropdownPartials

        [HttpGet]
        public ActionResult BooksByAuthor (int id = 0)
        {
            List<BookModel> model = librarianContext.Books(libId).Select(c => new BookModel
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

            return PartialView("~/Views/Shared/_BookDropdown.cshtml", model);
        }
        #endregion
    }
}