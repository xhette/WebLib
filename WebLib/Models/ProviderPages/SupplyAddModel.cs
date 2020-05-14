using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models.ProviderPages
{
	public class SupplyAddModel
	{
		public SupplyModel Supply { get; set; }

		public List<ShopModel> Shops { get; set; }

		public SupplyAddModel()
		{
			Supply = new SupplyModel();
			Supply.Date = DateTime.Today;
			Supply.Summ = 0;
			Shops = new List<ShopModel>();
		}

		public static explicit operator SupplyAddModel(SupplyDTO dto)
		{
			if (dto == null) return null;
			else
			{
				return new SupplyAddModel
				{
					Supply = new SupplyModel
					{
						Id = dto.Id,
						Date = dto.Date,
						ShopId = dto.ShopId,
						Summ = dto.Summ
					},
					Shops = new List<ShopModel>()
				};
			}
		}
	}
}