using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models.ReaderPages
{
	public class DepartmentListModel
	{
		public LibraryShortModel Library { get; set; }

		public List<DepartmentModel> DepartmentsIn { get; set; }

		public static explicit operator DepartmentListModel (DepartmentsGroupedDTO db)
		{
			if (db == null) return null;
			else
			{
				return new DepartmentListModel
				{
					Library = (LibraryShortModel)db.Library,
					DepartmentsIn = db.Departments.Select(c => (DepartmentModel)c).ToList()
				};
			}
		}
	}
}