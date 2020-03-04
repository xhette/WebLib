using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebLib.Enums
{
	public enum IssueStatusEnum
	{
		[Description("Выдано")]
		Processed = 1,

		[Description("Возвращено")]
		Returned = 2,

		[Description("Просрочено")]
		Spoiled = 3
	}
}