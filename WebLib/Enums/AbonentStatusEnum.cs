using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebLib.Enums
{
	public enum AbonentStatusEnum
	{
		[Description("не подана")]
		Null = 0,

		[Description("в рассмотрении")]
		Processing = 1,

		[Description("отклонена")]
		Declined = 2,

		[Description("принята")]
		Accepted = 3
	}
}