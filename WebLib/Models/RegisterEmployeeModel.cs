using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLib.Models
{
	public class RegisterEmployeeModel
	{
		public int EmployeeType { get; set; }

		[Display(Name = "Идентификатор сотрудника")]
		[Required(ErrorMessage = "Идентификатор сотрудника должнен быть установлен")]
		[Range(1, int.MaxValue, ErrorMessage = "Идентификатор сотрудника должнен быть больше 0")]
		public int EmployeeId { get; set; }
		public RegisterUserModel User { get; set; }

		public RegisterEmployeeModel()
		{
			User = new RegisterUserModel();
		}
	}
}