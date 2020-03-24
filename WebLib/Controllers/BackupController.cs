using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.DataLayer;

namespace WebLib.Controllers
{
    public class BackupController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BackupDb()
        {
            string path = Server.MapPath("~/Content/Backup");
            byte[] backup = BackupMethods.BackupDb(path);
            return File(backup, "application/zip", "backup.zip");
        }

        public ActionResult RestoreDb(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Server.MapPath("~/Content/Restore");
                string name = "LibraryDbRestore";
                string date = DateTime.Today.ToString("dd-MM-yyyy");

                string folderName = name + "-" + date;
                string filePath = String.Format("{0}\\{1}", path, folderName);
                Directory.CreateDirectory(filePath);
                file.SaveAs(filePath + "\\" + file.FileName);
                BackupMethods.RestoreDb(filePath);
            }

            return RedirectToAction("Index", "Backup");
        }
    }
}