using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models.ProviderPages
{
	public class SupplyViewModel
	{
		public SupplyModel Supply { get; set; }

		public ShopModel Shop { get; set; }

		public static explicit operator SupplyViewModel (SupplyDetailedDTO dto)
		{
			if (dto == null) return null;
			else return new SupplyViewModel
			{
				Shop = (ShopModel)dto.Shop,
				Supply = (SupplyModel)dto.Supply
			};
		}
	}

}