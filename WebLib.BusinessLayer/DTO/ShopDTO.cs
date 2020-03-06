using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class ShopDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public static explicit operator ShopDTO (Shop db)
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
	}
}
