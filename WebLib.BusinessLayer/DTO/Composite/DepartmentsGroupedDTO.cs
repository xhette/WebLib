using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class DepartmentsGroupedDTO
	{
		public LibraryShortDTO Library { get; set; }

		public List<DepartmentDTO> Departments { get; set; }

		public static explicit operator DepartmentsGroupedDTO (DepartmentGrouped db)
		{
			if (db == null) return null;
			else
			{
				return new DepartmentsGroupedDTO
				{
					Library = (LibraryShortDTO)db.Library,
					Departments = db.Departments.Select(c => (DepartmentDTO)c).ToList()
				};
			}
		}
	}
}
