using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.DataLayer.Procedures
{
	public class DepartmentGrouped
	{
		public Library Library { get; set; }

		public List<Department> Departments { get; set; }

		public DepartmentGrouped ()
		{
			Departments = new List<Department>();
		}
	}
}
