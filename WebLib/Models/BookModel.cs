using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class BookModel
	{
		public int Id { get; set; }

		public string Title { get; set; }


		public int AuthorId { get; set; }

		public int? DepartmentId { get; set; }

		public static explicit operator BookModel (BookDTO dto)
		{
			if (dto == null) return null;
			else return new BookModel
			{
				Id = dto.Id,
				AuthorId = dto.AuthorId,
				Title = dto.Title,
				DepartmentId = dto.DepartmentId
			};
		}
	}
}