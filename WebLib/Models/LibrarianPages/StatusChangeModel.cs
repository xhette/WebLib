using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLib.Enums;

namespace WebLib.Models.LibrarianPages
{
	public class StatusChangeModel
	{
		public int LibraryId { get; set; }

		public int ReaderId { get; set; }

		public AbonentStatusEnum Status { get; set; }

		public IEnumerable<SelectListItem> StatusList { get; set; }
	}
}