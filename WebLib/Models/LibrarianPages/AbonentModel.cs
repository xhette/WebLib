using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.Enums;
using WebLib.Models.ReaderPages;

namespace WebLib.Models.LibrarianPages
{
	public class AbonentModel
	{
		public string ReaderCard { get; set; }

		public ReaderDataModel Reader { get; set; }

		public AbonentStatusEnum Status { get; set; }

		public LibraryModel Library { get; set; }

		public static explicit operator AbonentModel (AbonentInLibraryDTO dto)
		{
			if (dto == null) return null;
			else return new AbonentModel
			{
				ReaderCard = dto.ReaderCard,
				Reader = new ReaderDataModel
				{
					Id = dto.Reader.Id,
					UserId = dto.Reader.UserId,
					Surname = dto.Reader.Surname,
					Name = dto.Reader.Name,
					Patronymic = dto.Reader.Patronymic
				},
				Status = (AbonentStatusEnum)dto.Status,
				Library = new LibraryModel
				{
					Id = dto.Library.Id,
					CityId = dto.Library.CityId,
					CityName = dto.Library.CityName,
					Name = dto.Library.Name,
					Address = dto.Library.Name,
					Phone = dto.Library.Phone
				}
			};
		}
	}
}