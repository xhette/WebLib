using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLib.Models.ReaderPages
{
	public class ChangePasswordModel
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "Введите старый пароль")]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		[Required(ErrorMessage ="Введите новый пароль")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}