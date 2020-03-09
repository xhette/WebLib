using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class BookDetailedDTO
	{
		public int AuthorId { get; set; }

		public string AuthorSurname { get; set; }

		public string AuthorName { get; set; }

		public string AuthorPatronymic { get; set; }

		public int BookId { get; set; }

		public string Title { get; set; }

		public int DepartmentId { get; set; }

		public string DepartmentName { get; set; }

		public int LibraryId { get; set; }

		public string LibraryName { get; set; }

		public static explicit operator BookDetailedDTO (BookDetailed db)
		{
			if (db == null) return null;
			else return new BookDetailedDTO
			{
				AuthorId = db.AuthorId,
				AuthorSurname = db.AuthorSurname,
				AuthorName = db.AuthorName,
				AuthorPatronymic = db.AuthorPatronymic,

				BookId = db.BookId,
				Title = db.Title,

				DepartmentId = db.DepartmentId,
				DepartmentName = db.DepartmentName,

				LibraryId = db.LibraryId,
				LibraryName = db.LibraryName
			};
		}
	}
}
