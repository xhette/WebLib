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

		public int Status { get; set; }

		public ReaderShortDataDTO Reader { get; set; }

		public LibraryDTO Library { get; set; }

		public CityDTO City { get; set; }

		public static explicit operator AbonentInLibraryDTO (AbonentInLibrary dbAbonent)
		{
			if (dbAbonent == null) return null;
			else return new AbonentInLibraryDTO
			{
				ReaderCard = dbAbonent.ReaderCard,
				Status = dbAbonent.AbonentStatus,
				Reader = new ReaderShortDataDTO
				{
					Id = dbAbonent.ReaderId,
					Surname = dbAbonent.ReaderSurname,
					Name = dbAbonent.ReaderName,
					Patronymic = dbAbonent.ReaderPatronymic
				},
				Library = new LibraryDTO
				{
					Id = dbAbonent.LibraryId,
					Name = dbAbonent.LibraryName
				}
			};
		}

		public static explicit operator AbonentInLibraryDTO (AbonentsInLibrariesDetailed dbAbonent)
		{
			if (dbAbonent == null) return null;
			else return new AbonentInLibraryDTO
			{
				ReaderCard = dbAbonent.ReaderCard,
				Status = dbAbonent.AbonentStatus.HasValue ? dbAbonent.AbonentStatus.Value : 0,
				Reader = new ReaderShortDataDTO
				{
					Id = dbAbonent.ReaderId.HasValue ? dbAbonent.ReaderId.Value : 0,
					Surname = dbAbonent.ReaderSurname,
					Name = dbAbonent.ReaderName,
					Patronymic = dbAbonent.ReaderPatronymic
				},
				Library = new LibraryDTO
				{
					Id = dbAbonent.LibraryId,
					Name = dbAbonent.LibraryName,
					Address = dbAbonent.LibAddress,
					Phone = dbAbonent.LibPhone
				},
				City = new CityDTO
				{
					Id = dbAbonent.CityId,
					Name = dbAbonent.CityName
				}
			};
		}
	}
}
