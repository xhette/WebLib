using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models.ReaderPages
{
	public class ReaderDataModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string FullName
        {
            get
            {
                return $"{ Surname } { Name } { Patronymic }";
            }
        }

        public DateTime? BirthDate { get; set; }

        public string BirthDateString
        {
            get
            {
                return BirthDate.HasValue ? BirthDate.Value.ToShortDateString() : String.Empty;
            }
        }

        public string PassSeria { get; set; }

        public string PassNumber { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }


        public static explicit operator ReaderDataModel (ReaderDataDTO dbReader)
        {
            if (dbReader == null) return null;
            else return new ReaderDataModel
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