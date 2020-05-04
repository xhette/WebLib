using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.Models.ReaderPages;

namespace WebLib.Models
{
	public class RegisterReaderModel
	{
		public ReaderDataModel Reader { get; set; }

		public RegisterUserModel User { get; set; }

		public RegisterReaderModel()
		{
			Reader = new ReaderDataModel();
			User = new RegisterUserModel();
		}
	}
}