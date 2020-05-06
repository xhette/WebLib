using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables;
using WebLib.Models;

namespace WebLib.Controllers
{

    [Authorize(Roles = "admin")]
    public class HistoryController : Controller
    {
        private IHistory history;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authors(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult AuthorsUndo(DateTime time, int step)
        {
            history = new HistoryAuthors();
            step = history.Undone(step, time);

            return RedirectToAction("Authors", "History", new { time = time, step = step });
        }

        public ActionResult AuthorsRedo(DateTime time, int step)
        {
            history = new HistoryAuthors();
            step = history.Redone(step, time);

            return RedirectToAction("Authors", "History", new { time = time, step = step });
        }

        public ActionResult Books(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult BooksUndo(DateTime time, int step)
        {
            history = new HistoryBooks();
            step = history.Undone(step, time);

            return RedirectToAction("Books", "History", new { time = time, step = step });
        }

        public ActionResult BooksRedo(DateTime time, int step)
        {
            history = new HistoryBooks();
            step = history.Redone(step, time);

            return RedirectToAction("Books", "History", new { time = time, step = step });
        }
        public ActionResult Cities(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult CitiesUndo(DateTime time, int step)
        {
            history = new HistoryCities();
            step = history.Undone(step, time);

            return RedirectToAction("Cities", "History", new { time = time, step = step });
        }

        public ActionResult CitiesRedo(DateTime time, int step)
        {
            history = new HistoryCities();
            step = history.Redone(step, time);

            return RedirectToAction("Cities", "History", new { time = time, step = step });
        }
        public ActionResult Departments(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult DepartmentsUndo(DateTime time, int step)
        {
            history = new HistoryDepartments();
            step = history.Undone(step, time);

            return RedirectToAction("Departments", "History", new { time = time, step = step });
        }

        public ActionResult DepartmentsRedo(DateTime time, int step)
        {
            history = new HistoryDepartments();
            step = history.Redone(step, time);

            return RedirectToAction("Departments", "History", new { time = time, step = step });
        }
        public ActionResult Issues(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult IssuesUndo(DateTime time, int step)
        {
            history = new HistoryIssues();
            step = history.Undone(step, time);

            return RedirectToAction("Issues", "History", new { time = time, step = step });
        }

        public ActionResult IssuesRedo(DateTime time, int step)
        {
            history = new HistoryIssues();
            step = history.Redone(step, time);

            return RedirectToAction("Issues", "History", new { time = time, step = step });
        }
        public ActionResult Librarians(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult LibrariansUndo(DateTime time, int step)
        {
            history = new HistoryLibrarians();
            step = history.Undone(step, time);

            return RedirectToAction("Librarians", "History", new { time = time, step = step });
        }

        public ActionResult LibrariansRedo(DateTime time, int step)
        {
            history = new HistoryLibrarians();
            step = history.Redone(step, time);

            return RedirectToAction("Librarians", "History", new { time = time, step = step });
        }
        public ActionResult Libraries(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult LibrariesUndo(DateTime time, int step)
        {
            history = new HistoryLibraries();
            step = history.Undone(step, time);

            return RedirectToAction("Libraries", "History", new { time = time, step = step });
        }

        public ActionResult LibrariesRedo(DateTime time, int step)
        {
            history = new HistoryLibraries();
            step = history.Redone(step, time);

            return RedirectToAction("Libraries", "History", new { time = time, step = step });
        }

        public ActionResult Providers(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult ProvidersUndo(DateTime time, int step)
        {
            history = new HistoryProviders();
            step = history.Undone(step, time);

            return RedirectToAction("Providers", "History", new { time = time, step = step });
        }

        public ActionResult ProvidersRedo(DateTime time, int step)
        {
            history = new HistoryProviders();
            step = history.Redone(step, time);

            return RedirectToAction("Providers", "History", new { time = time, step = step });
        }

        public ActionResult Readers(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult ReadersUndo(DateTime time, int step)
        {
            history = new HistoryReaders();
            step = history.Undone(step, time);

            return RedirectToAction("Readers", "History", new { time = time, step = step });
        }

        public ActionResult ReadersRedo(DateTime time, int step)
        {
            history = new HistoryReaders();
            step = history.Redone(step, time);

            return RedirectToAction("Readers", "History", new { time = time, step = step });
        }

        public ActionResult Shops(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult ShopsUndo(DateTime time, int step)
        {
            history = new HistoryLibraries();
            step = history.Undone(step, time);

            return RedirectToAction("Shops", "History", new { time = time, step = step });
        }

        public ActionResult ShopsRedo(DateTime time, int step)
        {
            history = new HistoryShops();
            step = history.Redone(step, time);

            return RedirectToAction("Shops", "History", new { time = time, step = step });
        }

        public ActionResult Supplies(DateTime? time, int? step)
        {
            TempworkModel model = new TempworkModel();

            if (time.HasValue && step.HasValue)
            {
                model.Time = time.Value;
                model.Step = step.Value;
            }

            return View(model);
        }

        public ActionResult SuppliesUndo(DateTime time, int step)
        {
            history = new HistorySupplies();
            step = history.Undone(step, time);

            return RedirectToAction("Supplies", "History", new { time = time, step = step });
        }

        public ActionResult SuppliesRedo(DateTime time, int step)
        {
            history = new HistorySupplies();
            step = history.Redone(step, time);

            return RedirectToAction("Supplies", "History", new { time = time, step = step });
        }

        public ActionResult Save(string table = "Index")
        {
            return RedirectToAction(table, "History");
        }
    }
}