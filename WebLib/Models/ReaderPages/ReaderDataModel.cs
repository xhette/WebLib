using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models.ReaderPages
{
	public class ReaderDataModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage ="Введите фамилию")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }

        public string FullName
        {
            get
            {
                return $"{ Surname } { Name } { Patronymic }";
            }
        }

        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string BirthDateString
        {
            get
            {
                return BirthDate.HasValue ? BirthDate.Value.ToString("dd.MM.yyyy") : String.Empty;
            }
        }

        [Required(ErrorMessage = "Введите серию паспорта")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid")]
        public string PassSeria { get; set; }

        [Required(ErrorMessage = "Введите номер паспорта")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid")]
        public string PassNumber { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^\+7 \(\d{3}\) \d{3} - \d{2} - \d{2}$", ErrorMessage = "Invalid Phone Number")]
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

        public static explicit operator ReaderDataDTO (ReaderDataModel dbReader)
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
    }
}