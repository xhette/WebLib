using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class AbonentInLibraryDTO
	{
		public string ReaderCard { get; set; }

		public ReaderShortDataDTO Reader { get; set; }
		public LibraryShortDTO Library { get; set; }

		public static explicit operator AbonentInLibraryDTO (AbonentInLibrary dbAbonent)
		{
			if (dbAbonent == null) return null;
			else return new AbonentInLibraryDTO
			{
				ReaderCard = dbAbonent.ReaderCard,
				Reader = new ReaderShortDataDTO
				{
					Id = dbAbonent.ReaderId,
					Surname = dbAbonent.ReaderSurname,
					Name = dbAbonent.ReaderName,
					Patronymic = dbAbonent.ReaderPatronymic
				},
				Library = new LibraryShortDTO
				{
					Id = dbAbonent.LibraryId,
					Name = dbAbonent.LibraryName
				}
			};
		}
	}
}
