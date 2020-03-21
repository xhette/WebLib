using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class SupplyModel
	{
		public int Id { get; set; }

		public DateTime? Date { get; set; }

		public decimal? Summ { get; set; }

		public int ShopId { get; set; }

		public static explicit operator SupplyModel (SupplyDTO dto)
		{
			if (dto == null) return null;
			else return new SupplyModel
			{
				Id = dto.Id,
				ShopId = dto.ShopId,
				Date = dto.Date,
				Summ = dto.Summ
			};
		}

		public static explicit operator SupplyDTO(SupplyModel db)
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
	}
}