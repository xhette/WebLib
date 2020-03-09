using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class AuthorModel
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1} {2}",
                    Surname,
                    Name,
                    String.IsNullOrEmpty(Patronymic) ? String.Empty : Patronymic);
            }
        }

        public static explicit operator AuthorModel (AuthorDTO dbAuthor)
        {
            if (dbAuthor == null) return null;
            else return new AuthorModel
            {
                Id = dbAuthor.Id,
                Surname = dbAuthor.Surname,
                Name = dbAuthor.Name,
                Patronymic = dbAuthor.Patronymic
            };
        }

    }
}