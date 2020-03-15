using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.DTO
{
	public class CityDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static explicit operator CityDTO (Cities db)
		{
			if (db == null) return null;
			else
			{
				return new CityDTO
				{
					Id = db.Id,
					Name = db.Name
				};
			}
		}

		public static explicit operator Cities (CityDTO db)
		{
			if (db == null) return null;
			else
			{
				return new Cities
				{
					Id = db.Id,
					Name = db.Name
				};
			}
		}
	}
}
