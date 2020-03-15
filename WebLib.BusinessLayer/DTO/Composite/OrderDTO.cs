using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class OrderDTO
	{
		public BookDetailedDTO Book { get; set; }

		public SupplyDTO Supply { get; set; }

		public ShopDTO Shop { get; set; }

		public static explicit operator OrderDTO (Orders db)
		{
			if (db == null) return null;
			else return new OrderDTO
			{
				Book = new BookDetailedDTO
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
				},
				Supply = new SupplyDTO
				{
					Id = db.SupplyId,
					ShopId = db.ShopId,
					Date = db.Date,
					Summ = db.SupplySumm
				},
				Shop = new ShopDTO
				{
					Id = db.ShopId,
					Name = db.ShopName
				}
			};
		}
	}
}
