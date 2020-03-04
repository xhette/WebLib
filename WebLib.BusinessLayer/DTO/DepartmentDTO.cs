using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class DepartmentDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int LibraryId { get; set; }

		public static explicit operator DepartmentDTO (Department dbDepartment)
		{
			if (dbDepartment == null) return null;
			else return new DepartmentDTO
			{
				Id = dbDepartment.Id,
				Name = dbDepartment.Name,
				LibraryId = dbDepartment.LibraryId
			};
		}
	}
}
