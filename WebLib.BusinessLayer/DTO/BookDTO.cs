using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.DTO
{
	public class BookDTO
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int AuthorId { get; set; }

		public int? DepartmentId { get; set; }

		public static explicit operator BookDTO (Books dbBook)
		{
			if (dbBook == null) return null;
			else return new BookDTO
			{
				Id = dbBook.Id,
				AuthorId = dbBook.Author,
				DepartmentId = dbBook.Department,
				Title = dbBook.Title
			};
		}

		public static explicit operator Books (BookDTO dbBook)
		{
			if (dbBook == null) return null;
			else return new Books
			{
				Id = dbBook.Id,
				Author = dbBook.AuthorId,
				Department = dbBook.DepartmentId,
				Title = dbBook.Title
			};
		}
	}
}
