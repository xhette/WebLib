using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.Models;
using WebLib.Models.ReaderPages;

namespace WebLib.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Authors()
        {
            return View();
        }

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