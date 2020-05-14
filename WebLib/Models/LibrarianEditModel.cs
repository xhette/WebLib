using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class LibrarianEditModel
	{
		public LibrarianModel Librarian { get; set; }

		public List<LibraryModel> Libraries { get; set; }

		public LibrarianEditModel()
		{
			Libraries = new List<LibraryModel>();
		}

		public static explicit operator LibrarianEditModel(LibrarianDataDTO dto)
		{
			if (dto == null) return null;
			else return new LibrarianEditModel
			{
				Librarian = new LibrarianModel
				{
					Id = dto.Id,
					Surname = dto.Surname,
					Name = dto.Name,
					Patronymic = dto.Patronymic,
					Address = dto.Address,
					Phone = dto.Phone,
					UserId = dto.UserId,
					LibraryId = dto.LibraryId
				},

				Libraries = new List<LibraryModel>()
			};
		}

		public static explicit operator LibrarianDataDTO(LibrarianEditModel dto)
		{
			if (dto == null) return null;
			else return new LibrarianDataDTO
			{
				Id = dto.Librarian.Id,
				Surname = dto.Librarian.Surname,
				Name = dto.Librarian.Name,
				Patronymic = dto.Librarian.Patronymic,
				Address = dto.Librarian.Address,
				Phone = dto.Librarian.Phone,
				UserId = dto.Librarian.UserId,
				LibraryId = dto.Librarian.LibraryId
			};
		}
	}
}