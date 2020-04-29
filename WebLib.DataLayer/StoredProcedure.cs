using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Procedures;

namespace WebLib.DataLayer
{
	public class StoredProcedure
	{
		private LibContext context;

		#region Get
		public StoredProcedure (LibContext dbContext)
		{
			context = dbContext;
		}

		public List<IssueDetailed> IssueList ()
		{
			var issues = context.Database.SqlQuery<IssueDetailed>("IssueList").ToList();

			return issues;
		}

		public List<AbonentInLibrary> Abonents ()
		{
			var abonents = context.Database.SqlQuery<AbonentInLibrary>("AbonentsInLibraries").ToList();

			return abonents;
		}

		public List<AbonentInLibrary> AbonentsSpoiled()
		{
			var abonents = context.Database.SqlQuery<AbonentInLibrary>("AbonentsInLibrariesSpoiled").ToList();

			return abonents;
		}

		public List<AbonentsInLibrariesDetailed> LibrariesWithAbonents ()
		{
			var abonents = context.Database.SqlQuery<AbonentsInLibrariesDetailed>("AbonentsInLibrariesDetailed").ToList();

			return abonents;
		}

		public List<Orders> OrderList ()
		{
			var orders = context.Database.SqlQuery<Orders>("OrdersList").ToList();

			return orders;
		}

		public List<LibraryDetailed> LibraryDetailedList ()
		{
			var libraries = context.Database.SqlQuery<LibraryDetailed>("LibraryDetailedList").ToList();

			return libraries;
		}

		public List <DepartmentGrouped> DepartmentList ()
		{
			var departments = context.Libraries.GroupJoin(context.Departments, lib => lib.Id, dept => dept.Library, (lib, dept) => new DepartmentGrouped
			{
				Library = lib,
				Departments = dept.ToList()
			}).ToList();

			return departments;
		}

		public List<DepartmentGrouped> DepartmentList (string symbols)
		{
			var departments = context.Libraries.GroupJoin(context.Departments, lib => lib.Id, dept => dept.Library, (lib, dept) => new DepartmentGrouped
			{
				Library = lib,
				Departments = dept.Where(c => c.Name.Contains(symbols)).ToList()
			}).ToList();

			return departments;
		}

		public List<BookDetailed> BookList ()
		{
			var books = context.Database.SqlQuery<BookDetailed>("ListBook").ToList();

			return books;
		}

		public List<BookDetailed> ProviderBookList()
		{
			var books = context.Database.SqlQuery<BookDetailed>("ProviderListBook").ToList();

			return books;
		}

		public List<LibrarianDetailed> LibrarianList()
		{
			var librarians = context.Database.SqlQuery<LibrarianDetailed>("LibrariansDetailed").ToList();

			return librarians;
		}
		#endregion
	}
}
