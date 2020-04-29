using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLib.Models.ProviderPages
{
	public class AddOrderModel
	{
		public int SupplyId { get; set; }

		public List<AuthorModel> Authors { get; set; }

		public int SelectedAuthor { get; set; }

		public int BookId { get; set; }

		public string Title { get; set; }

		public double Cost { get; set; } 

		public AddOrderModel()
		{
			Authors = new List<AuthorModel>();
		}
	}
}