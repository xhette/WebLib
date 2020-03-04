using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models.ReaderPages
{
	public class BookViewModel
	{
		public int BookId { get; set; }

		public string Title { get; set; }


		public int AuthorId { get; set; }

		public string AuthorSurname { get; set; }

		public string AuthorName { get; set; }

		public string AuthorPatronymic { get; set; }

		public string AuthorFullName
		{
			get
			{
				return String.Format("{0} {1} {2}",
					AuthorSurname,
					AuthorName,
					String.IsNullOrEmpty(AuthorPatronymic) ? String.Empty : AuthorPatronymic);
			}
		}


		public int? DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		public int LibraryId { get; set; }
		public string LibraryName { get; set; }

		public static explicit operator BookViewModel (BookDetailedDTO bookDTO)
		{
			if (bookDTO == null) return null;
			else return new BookViewModel
			{
				AuthorId = bookDTO.AuthorId,
				AuthorSurname = bookDTO.AuthorSurname,
				AuthorName = bookDTO.AuthorName,
				AuthorPatronymic = bookDTO.AuthorPatronymic,

				BookId = bookDTO.BookId,
				Title = bookDTO.Title,

				DepartmentId = bookDTO.DepartmentId,
				DepartmentName = bookDTO.DepartmentName,

				LibraryId = bookDTO.LibraryId,
				LibraryName = bookDTO.LibraryName
			};
		}
	}
}