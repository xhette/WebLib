using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class SupplyDTO
	{
		public int Id { get; set; }

		public DateTime? Date { get; set; }

		public decimal? Summ { get; set; }

		public int ShopId { get; set; }

		public static explicit operator SupplyDTO (Supply db)
		{
			if (db == null) return null;
			else return new SupplyDTO
			{
				Id = db.Id,
				ShopId = db.ShopId,
				Date = db.Date,
				Summ = db.Summ
			};
		}

		public static explicit operator Supply (SupplyDTO db)
		{
			if (db == null) return null;
			else return new Supply
			{
				Id = db.Id,
				ShopId = db.ShopId,
				Date = db.Date,
				Summ = db.Summ
			};
		}
	}
}
