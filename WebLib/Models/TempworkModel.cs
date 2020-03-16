using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLib.Models
{
	public class TempworkModel
	{
		public int Step { get; set; }

		public DateTime Time { get; set; }

		public TempworkModel()
		{
			this.Step = 0;
			this.Time = DateTime.Now;
		}
	}
}