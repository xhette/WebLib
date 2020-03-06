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
			List<CityModel> cities;
            using (context = new LibDbContext())
            {
                readerContext = new ReaderPage(context);
                model = readerContext.LibraryList().Select(c => (LibraryModel)c).ToList();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LibrariesByCity (int? cityId)
        {
            List<LibraryModel> model = new List<LibraryModel>();
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
	}
}