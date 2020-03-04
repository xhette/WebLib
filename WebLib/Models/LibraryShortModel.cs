using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models
{
	public class LibraryShortModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static explicit operator LibraryShortModel (LibraryShortDTO dto)
		{
			if (dto == null) return null;
			else
			{
				return new LibraryShortModel
				{
					Id = dto.Id,
					Name = dto.Name
				};
			}
		}

		public static explicit operator LibraryShortModel (AbonentInLibraryDTO dto)
		{
			if (dto == null) return null;
			else
			{
				return new LibraryShortModel
				{
					Id = dto.Library.Id,
					Name = dto.Library.Name
				};
			}
		}
	}
}