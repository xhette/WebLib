using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.DTO
{
	public class SupplyDTO
	{
		public int Id { get; set; }

		public DateTime? Date { get; set; }

		public decimal? Summ { get; set; }

		public int ShopId { get; set; }
	}
}
