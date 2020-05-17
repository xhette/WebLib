using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods.AdminPages;
using WebLib.DataLayer;

namespace WebLib.Controllers
{

    [Authorize(Roles = "admin")]
    public class BackupController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Backup()
        {
            string path = Server.MapPath("~/Content/Backup");
            byte[] backup = BackupMethods.BackupDb(path);
            return File(backup, "application/zip", "backup.zip");
        }

        public ActionResult Restore(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Server.MapPath("~/Content/Restore");
                string filePath = String.Format("{0}\\{1}", path, Guid.NewGuid());
                Directory.CreateDirectory(filePath);
                file.SaveAs(filePath + "\\" + file.FileName);
                BackupMethods.RestoreDb(filePath);
            }

            return RedirectToAction("Index", "Backup");
        }
    }
}