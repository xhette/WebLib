using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.DataLayer.Procedures
{
	public class BookDetailed
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
	}
}
