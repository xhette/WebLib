using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.DataLayer.Procedures
{
	public class Orders
	{
		public int BookId { get; set; }

		public int AuthorId { get; set; }

		public string AuthorSurname { get; set; }

		public string AuthorName { get; set; }

		public string AuthorPatronymic { get; set; }

		public string Title { get; set; }

		public double Cost { get; set; }

		public int SupplyId { get; set; }

		public DateTime Date { get; set; }

		public double SupplySumm { get; set; }

		public int ShopId { get; set; }

		public string ShopName { get; set; }
	}
}
