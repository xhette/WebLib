using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Base;

namespace WebLib.DataLayer.Procedures
{
	public class DepartmentGrouped
	{
		public Libraries Library { get; set; }

		public List<Departments> Departments { get; set; }

		public DepartmentGrouped ()
		{
			Departments = new List<Departments>();
		}
	}
}
