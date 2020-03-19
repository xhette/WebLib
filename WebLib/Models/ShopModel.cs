using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class ShopModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public static explicit operator ShopDTO(ShopModel db)
		{
			if (db == null) return null;
			else return new ShopDTO
			{
				Id = db.Id,
				Name = db.Name,
				Address = db.Address,
				Phone = db.Phone
			};
		}

		public static explicit operator ShopModel(ShopDTO db)
		{
			if (db == null) return null;
			else return new ShopModel
			{
				Id = db.Id,
				Name = db.Name,
				Address = db.Address,
				Phone = db.Phone
			};
		}
	}
}