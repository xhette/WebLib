using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.Enums;

namespace WebLib.Models.ReaderPages
{
	public class LibraryReaderModel
	{
		public AbonentStatusEnum AbonentStatus { get; set; }

		public LibraryModel Library { get; set; }

		public CityModel City { get; set; }

		public static explicit operator LibraryReaderModel (AbonentInLibraryDTO dto)
		{
			if (dto == null) return null;
			else return new LibraryReaderModel
			{
				AbonentStatus = (AbonentStatusEnum)dto.Status,
				Library = (LibraryModel)dto.Library,
				City = (CityModel)dto.City
			};
		}
	}
}