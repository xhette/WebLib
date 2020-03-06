using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLib.Models.ReaderPages
{
	public class DepartmentListModel
	{
		public LibraryShortModel Library { get; set; }

		public List<DepartmentModel> DepartmentsIn { get; set; }
	}
}