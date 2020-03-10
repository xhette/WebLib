﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class LibrarianDataDTO
	{
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int LibraryId { get; set; }

        public int? UserId { get; set; }

        public static explicit operator LibrarianDataDTO (Librarian db)
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
    }
}
