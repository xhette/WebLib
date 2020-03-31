using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLib.Models.ReaderPages;

namespace WebLib.Models.LibrarianPages
{
	public class AddIssueModel
	{
		public int BookId { get; set; }

		[Required(ErrorMessage ="Выберите читателя")]
		public int ReaderId { get; set; }

		public List<ReaderDataModel> Readers { get; set; } 
	}
}