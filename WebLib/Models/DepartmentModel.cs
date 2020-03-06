using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class DepartmentModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int LibraryId { get; set; }

		public static explicit operator DepartmentModel (DepartmentDTO dto)
		{
			if (dto == null) return null;
			else return new DepartmentModel
			{
				Id = dto.Id,
				Name = dto.Name,
				LibraryId = dto.LibraryId
			};
		}

	}
}