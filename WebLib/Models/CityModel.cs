using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class CityModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static explicit operator CityModel (CityDTO dto)
		{
			if (dto == null) return null;
			else
			{
				return new CityModel
				{
					Id = dto.Id,
					Name = dto.Name
				};
			}
		}
	}
}