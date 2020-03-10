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
		private LibDbContext context;

		#region Get
		public StoredProcedure (LibDbContext dbContext)
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

		public List<AbonentsInLibrariesDetailed> LibrariesWithAbonents ()
		{
			var abonents = context.Database.SqlQuery<AbonentsInLibrariesDetailed>("AbonentsInLibrariesDetailed").ToList();

			return abonents;
		}

		public List<Order> OrderList ()
		{
			var orders = context.Database.SqlQuery<Order>("OrdersList").ToList();

			return orders;
		}

		public List<LibraryDetailed> LibraryDetailedList ()
		{
			var libraries = context.Database.SqlQuery<LibraryDetailed>("LibraryDetailedList").ToList();

			return libraries;
		}

		public List <DepartmentGrouped> DepartmentList ()
		{
			var departments = context.Library.GroupJoin(context.Department, lib => lib.Id, dept => dept.Library, (lib, dept) => new DepartmentGrouped
			{
				Library = lib,
				Departments = dept.ToList()
			}).ToList();

			return departments;
		}

		public List<DepartmentGrouped> DepartmentList (string symbols)
		{
			var departments = context.Library.GroupJoin(context.Department, lib => lib.Id, dept => dept.Library, (lib, dept) => new DepartmentGrouped
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
		#endregion
	}
}
