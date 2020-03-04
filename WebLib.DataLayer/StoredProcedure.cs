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
	}
}
