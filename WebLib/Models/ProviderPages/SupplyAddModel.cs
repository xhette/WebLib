using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
	}
}