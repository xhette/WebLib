using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables;
using WebLib.Models;

namespace WebLib.Controllers
{
    public class HistoryController : Controller
    {
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
            step = HistoryAuthors.Undone(step, time);

            return RedirectToAction("Authors", "History", new { time = time, step = step });
        }

        public ActionResult AuthorsRedo(DateTime time, int step)
        {
            step = HistoryAuthors.Redone(step, time);

            return RedirectToAction("Authors", "History", new { time = time, step = step });
        }
    }
}