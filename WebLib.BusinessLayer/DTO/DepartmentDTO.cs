using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.DTO
{
	public class DepartmentDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int LibraryId { get; set; }

		public static explicit operator DepartmentDTO (Departments dbDepartment)
		{
			if (dbDepartment == null) return null;
			else return new DepartmentDTO
			{
				Id = dbDepartment.Id,
				Name = dbDepartment.Name,
				LibraryId = dbDepartment.Library
			};
		}

		public static explicit operator Departments(DepartmentDTO dbDepartment)
		{
			if (dbDepartment == null) return null;
			else return new Departments
			{
				Id = dbDepartment.Id,
				Name = dbDepartment.Name,
				Library = dbDepartment.LibraryId
			};
		}
	}
}
