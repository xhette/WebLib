using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class OrderDTO
	{
		public BookDetailedDTO Book { get; set; }

		public SupplyDTO Supply { get; set; }

		public ShopDTO Shop { get; set; }
	}
}
