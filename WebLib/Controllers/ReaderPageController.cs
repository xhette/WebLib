using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods;
using WebLib.DataLayer;
using WebLib.Models;
using WebLib.Models.ReaderPages;

namespace WebLib.Controllers
{
    public class ReaderPageController : Controller
    {
        private ReaderPage readerContext;
        private LibDbContext context;
		private int userId = 28;


		public ActionResult Index()
        {
            ReaderPageModel model = new ReaderPageModel();
            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);
                model.Reader = (ReaderDataModel)readerContext.ReaderData(userId);
                model.Libraries = readerContext.AbonentList(model.Reader.Id).Select(c => (LibraryShortModel)c).ToList();
                var dbIssues = readerContext.ReaderIssuesList(model.Reader.Id).ToList();
                if (dbIssues.Count > 5)
                    dbIssues.Skip(Math.Max(0, dbIssues.Count() - 5));

                model.Issues = dbIssues.Select(c => (ReaderIssueModel)c).ToList();
            }
            return View(model);
        }

        public ActionResult Libraries()
        {
            List<LibraryModel> model;
			List<SelectListItem> cities;
            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);
                cities = readerContext.CityList().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                model = readerContext.LibraryList().Select(c => (LibraryModel)c).ToList();
            }
            ViewBag.CitySelectList = cities;
            
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult LibrariesByCity (int? cityId)
        {
            List<LibraryModel> model;
            using (context = new LibDbContext())
            {

                readerContext = new ReaderPage(context);
                if (cityId.HasValue)
                {
                    model = readerContext.LibraryList(cityId.Value).Select(c => (LibraryModel)c).ToList();
                }
                else
                {
                    model = readerContext.LibraryList().Select(c => (LibraryModel)c).ToList();
                }
            }

            return PartialView("~/Views/ReaderPage/_LibrariesByCity.cshtml", model);
        }

		public ActionResult Issues ()
		{
			List<ReaderIssueModel> model;
			using (context = new LibDbContext())
			{
				int readerId = context.Reader.FirstOrDefault(c => c.UserId == userId).Id;
				readerContext = new ReaderPage(context);
				var dbIssues = readerContext.ReaderIssuesList(readerId).ToList();

				model = dbIssues.Select(c => (ReaderIssueModel)c).ToList();
			}
			return View(model);
		}

        public ActionResult Books (int page = 1)
        {
            ViewBag.SearchingType = page;

            return View();
        }

        public ActionResult BooksByTitle ()
        {
            List<BookViewModel> model;

            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);
                model = readerContext.Books().Select(c => (BookViewModel)c).ToList();
            }
            return PartialView("~/Views/ReaderPage/_BooksByTitle.cshtml", model);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult BookSearch (string symbols)
        {
            List<BookViewModel> model;

            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);

                if (!String.IsNullOrEmpty(symbols))
                {
                    model = readerContext.Books().Where(c => c.Title.Contains(symbols)).Select(c => (BookViewModel)c).ToList();
                }
                else
                {
                    model = readerContext.Books().Select(c => (BookViewModel)c).ToList();
                }

            }

            ViewBag.TableHead = true;
            return PartialView("~/Views/ReaderPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByAuthor ()
        {
            List<AuthorModel> model;

            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);
                model = readerContext.Authors().Select(c => (AuthorModel)c).ToList();
            }
            return PartialView("~/Views/ReaderPage/_BooksByAuthor.cshtml", model);

        }

        public ActionResult BookSearchByAuthor (int authorId)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);

                if (authorId > 0)
                {
                    model = readerContext.Books().Where(c => c.AuthorId == authorId).Select(c => (BookViewModel)c).ToList();
                }

            }

            ViewBag.TableHead = false;
            return PartialView("~/Views/ReaderPage/_BookSearch.cshtml", model);
        }

        public ActionResult BooksByDepartment ()
        {
            List<DepartmentListModel> model = new List<DepartmentListModel>();

            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);
                model = readerContext.Departments().Select(c => (DepartmentListModel)c).ToList();
            }

            return PartialView("~/Views/ReaderPage/_BooksByDepartment.cshtml", model);
        }

        public ActionResult BookSearchByDepartment (int deptId)
        {
            List<BookViewModel> model = new List<BookViewModel>();

            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);

                if (deptId > 0)
                {
                    model = readerContext.Books().Where(c => c.DepartmentId == deptId).Select(c => (BookViewModel)c).ToList();
                }

            }
            ViewBag.TableHead = false;
            return PartialView("~/Views/ReaderPage/_BookSearch.cshtml", model);
        }
    }
}