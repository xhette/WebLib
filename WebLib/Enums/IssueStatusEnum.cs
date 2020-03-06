using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebLib.Enums
{
	public enum IssueStatusEnum
	{
		[Description("выдано")]
		Processed = 1,

		[Description("возвращено")]
		Returned = 2,

		[Description("просрочено")]
		Spoiled = 3
	}
}