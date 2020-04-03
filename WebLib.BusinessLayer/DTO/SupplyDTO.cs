using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.DTO
{
	public class SupplyDTO
	{
		public int Id { get; set; }

		public DateTime? Date { get; set; }

		public double? Summ { get; set; }

		public int ShopId { get; set; }

		public static explicit operator SupplyDTO (Supplies db)
		{
			if (db == null) return null;
			else return new SupplyDTO
			{
				Id = db.Id,
				ShopId = db.Shop,
				Date = db.Date,
				Summ = db.Summ
			};
		}

		public static explicit operator Supplies (SupplyDTO db)
		{
			if (db == null) return null;
			else return new Supplies
			{
				Id = db.Id,
				Shop = db.ShopId,
				Date = db.Date,
				Summ = db.Summ
			};
		}
	}
}
