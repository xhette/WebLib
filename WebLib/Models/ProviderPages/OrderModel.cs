using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models.ProviderPages
{
	public class OrderModel
	{
		public AuthorModel Author { get; set; }

		public BookModel Book { get; set; }

		public double Cost { get; set; }

		public static explicit operator OrderModel (OrderDTO dto)
		{
			if (dto == null) return null;
			else return new OrderModel
			{
				Author = (AuthorModel)dto.Author,
				Book = (BookModel)dto.Book,
				Cost = dto.Cost
			};
		}
	}
}