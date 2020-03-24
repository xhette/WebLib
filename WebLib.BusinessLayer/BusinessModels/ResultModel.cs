using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.BusinessModels
{
	public class ResultModel
	{
		public OperationStatusEnum Code { get; set; }

		public string Message { get; set; }

		public ResultModel()
		{
			Code = OperationStatusEnum.Success;
			Message = "Операция проведена успешно";
		}
	}
}
