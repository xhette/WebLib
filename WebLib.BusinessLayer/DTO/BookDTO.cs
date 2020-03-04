using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class BookDTO
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int AuthorId { get; set; }

		public int? DepartmentId { get; set; }

		public static explicit operator BookDTO (Book dbBook)
		{
			if (dbBook == null) return null;
			else return new BookDTO
			{
				Id = dbBook.Id,
				AuthorId = dbBook.AuthorId,
				DepartmentId = dbBook.DepartmentId,
				Title = dbBook.Title
			};
		}
	}
}
