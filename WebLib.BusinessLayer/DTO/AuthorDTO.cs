using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class AuthorDTO
	{
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public static explicit operator AuthorDTO (Author dbAuthor)
        {
            if (dbAuthor == null) return null;
            else return new AuthorDTO
            {
                Id = dbAuthor.Id,
                Surname = dbAuthor.Surname,
                Name = dbAuthor.Name,
                Patronymic = dbAuthor.Patronymic
            };
        }
    }
}
