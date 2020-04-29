using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models
{
	public class BookModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Введите заголовок")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Выберите автора")]
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


		public static explicit operator BookModel(BookDetailedDTO dto)
		{
			if (dto == null) return null;
			else return new BookModel
			{
				Id = dto.BookId,
				AuthorId = dto.AuthorId,
				Title = dto.Title,
				DepartmentId = dto.DepartmentId
			};
		}
	}
}