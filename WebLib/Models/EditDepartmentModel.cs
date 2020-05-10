using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class EditDepartmentModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int LibraryId { get; set; }

		public List<LibraryModel> Libraries { get; set; }

		public EditDepartmentModel()
		{
			Libraries = new List<LibraryModel>();
		}

		public static explicit operator EditDepartmentModel(DepartmentDTO dto)
		{
			if (dto == null) return null;
			else return new EditDepartmentModel
			{
				Id = dto.Id,
				Name = dto.Name,
				LibraryId = dto.LibraryId,
				Libraries = new List<LibraryModel>()
			};
		}

		public static explicit operator DepartmentDTO(EditDepartmentModel dto)
		{
			if (dto == null) return null;
			else return new DepartmentDTO
			{
				Id = dto.Id,
				Name = dto.Name,
				LibraryId = dto.LibraryId
			};
		}
	}
}