using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models.ProviderPages
{
	public class BookEditModel
	{
		public BookModel Book { get; set; }

		public List<AuthorModel> Authors { get; set; }

		public List<LibraryModel> Libraries { get; set; }

		public List<DepartmentModel> Departments { get; set; }

		public int SelectedLib { get; set; }

		public BookEditModel()
		{
			Authors = new List<AuthorModel>();
			Libraries = new List<LibraryModel>();
			Departments = new List<DepartmentModel>();
		}

		public static explicit operator BookEditModel (BookDetailedDTO dto)
		{
			if (dto == null) return null;
			else return new BookEditModel
			{
				Book = new BookModel
				{
					Id = dto.BookId,
					AuthorId = dto.AuthorId,
					Title = dto.Title,
					DepartmentId = dto.DepartmentId
				},
				Authors = new List<AuthorModel>(),
				Libraries = new List<LibraryModel>(),
				Departments = new List<DepartmentModel>(),
				SelectedLib = dto.LibraryId
			};
		}

		public static explicit operator BookDTO(BookEditModel dto)
		{
			if (dto == null) return null;
			else return new BookDTO
			{
				Id = dto.Book.Id,
				AuthorId = dto.Book.AuthorId,
				Title = dto.Book.Title,
				DepartmentId = dto.Book.DepartmentId
			};
		}
	}
}