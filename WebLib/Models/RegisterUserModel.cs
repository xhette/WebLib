using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLib.Models
{
	public class RegisterUserModel
	{
		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Поле Логин должно быть установлено")]
		public string Login { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password, ErrorMessage = "Пароль")]
		[Required(ErrorMessage = "Поле Пароль должно быть установлено")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}