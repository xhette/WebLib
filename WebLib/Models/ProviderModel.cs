using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class ProviderModel
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Address { get; set; }

        public int? UserId { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1} {2}", Surname, Name, Patronymic);
            }
        }

        public static explicit operator ProviderDataDTO(ProviderModel db)
        {
            if (db == null) return null;
            else return new ProviderDataDTO
            {
                Id = db.Id,
                UserId = db.UserId,
                Surname = db.Surname,
                Name = db.Name,
                Patronymic = db.Patronymic,
                Address = db.Address
            };
        }

        public static explicit operator ProviderModel(ProviderDataDTO db)
        {
            if (db == null) return null;
            else return new ProviderModel
            {
                Id = db.Id,
                UserId = db.UserId,
                Surname = db.Surname,
                Name = db.Name,
                Patronymic = db.Patronymic,
                Address = db.Address
            };
        }
    }
}