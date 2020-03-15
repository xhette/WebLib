using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class LibraryShortDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static explicit operator LibraryShortDTO (Libraries db)
		{
			if (db == null) return null;
			else return new LibraryShortDTO
			{
				Id = db.Id,
				Name = db.Name
			};
		}
	}
}
