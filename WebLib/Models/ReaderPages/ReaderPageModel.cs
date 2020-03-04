using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLib.Models.ReaderPages
{
	public class ReaderPageModel
	{
		public ReaderDataModel Reader { get; set; }

		public List<ReaderIssueModel> Issues { get; set; }

		public List<LibraryShortModel> Libraries { get; set; }
	}
}