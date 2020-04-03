using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class SupplyDetailedDTO
	{
		public SupplyDTO Supply { get; set; }

		public ShopDTO Shop { get; set; }
	}
}
