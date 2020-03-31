using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class LibrarianModel
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int LibraryId { get; set; }

        public string LibraryName { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1} {2}", Surname, Name, Patronymic);
            }
        }

        public int? UserId { get; set; }

        public static explicit operator LibrarianDataDTO(LibrarianModel db)
        {
            if (db == null) return null;
            else return new LibrarianDataDTO
            {
                Id = db.Id,
                UserId = db.UserId,
                Surname = db.Surname,
                Name = db.Name,
                Patronymic = db.Patronymic,
                Address = db.Address,
                Phone = db.Phone,
                LibraryId = db.LibraryId
            };
        }

        public static explicit operator LibrarianModel(LibrarianDataDTO db)
        {
            if (db == null) return null;
            else return new LibrarianModel
            {
                Id = db.Id,
                UserId = db.UserId,
                Surname = db.Surname,
                Name = db.Name,
                Patronymic = db.Patronymic,
                Address = db.Address,
                Phone = db.Phone,
                LibraryId = db.LibraryId,
                LibraryName = db.LibraryName
            };
        }
    }
}