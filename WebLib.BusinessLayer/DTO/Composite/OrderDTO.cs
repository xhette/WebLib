﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Base;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class OrderDTO
	{
		public SupplyDTO Supply { get; set; }

		public AuthorDTO Author { get; set; }

		public BookDTO Book { get; set; }

		public double Cost { get; set; }

		public static explicit operator OrderDTO (Orders db)
		{
			if (db == null) return null;
			else return new OrderDTO
			{
				Author = new AuthorDTO
				{
					Id = db.AuthorId,
					Surname = db.AuthorSurname,
					Name = db.AuthorName,
					Patronymic = db.AuthorPatronymic,
				},
				Book = new BookDTO
				{
					AuthorId = db.AuthorId,

					Id = db.BookId,
					Title = db.Title
				},
				Supply = new SupplyDTO
				{
					Id = db.SupplyId,
					ShopId = db.ShopId,
					Date = db.Date,
					Summ = db.SupplySumm
				},
				Cost = db.Cost
			};
		}


		public static explicit operator OrderLists (OrderDTO db)
		{
			if (db == null) return null;
			else return new OrderLists
			{
				Book = db.Book.Id,
				Supply = db.Supply.Id,
				Cost = db.Cost
			};
		}
	}
}
