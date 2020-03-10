using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.DTO
{
	public class ReaderDataDTO
	{
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public DateTime? BirthDate { get; set; }

        public string PassSeria { get; set; }

        public string PassNumber { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public static explicit operator ReaderDataDTO (Reader dbReader)
        {
            if (dbReader == null) return null;
            else return new ReaderDataDTO
            {
                UserId = dbReader.UserId,
                Id = dbReader.Id,
                Surname = dbReader.Surname,
                Name = dbReader.Name,
                Patronymic = dbReader.Patronymic,
                PassSeria = dbReader.PassSeria,
                PassNumber = dbReader.PassNumber,
                BirthDate = dbReader.BirthDate,
                Address = dbReader.Address,
                Phone = dbReader.Phone
            };
        }

        public static explicit operator Reader (ReaderDataDTO dbReader)
        {
            if (dbReader == null) return null;
            else return new Reader
            {
                UserId = dbReader.UserId,
                Id = dbReader.Id,
                Surname = dbReader.Surname,
                Name = dbReader.Name,
                Patronymic = dbReader.Patronymic,
                PassSeria = dbReader.PassSeria,
                PassNumber = dbReader.PassNumber,
                BirthDate = dbReader.BirthDate,
                Address = dbReader.Address,
                Phone = dbReader.Phone
            };
        }
    }
}
