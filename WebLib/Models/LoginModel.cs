using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLib.Models
{
	public class LoginModel
	{
		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Поле Логин должно быть установлено")]
		public string Login { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password, ErrorMessage = "Пароль")]
		[Required(ErrorMessage = "Поле Пароль должно быть установлено")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня?")]
		public bool RememberMe { get; set; }
	}
}