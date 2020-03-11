using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO
{
	public class LibraryDTO
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public static explicit operator LibraryDTO (Library dbLibrary)
        {
            if (dbLibrary == null) return null;
            else return new LibraryDTO
            {
                Id = dbLibrary.Id,
                Name = dbLibrary.Name,
                Address = dbLibrary.Address,
                Phone = dbLibrary.Phone,
                CityId = dbLibrary.City,
                CityName = String.Empty
            };
        }

        public static explicit operator Library (LibraryDTO dbLibrary)
        {
            if (dbLibrary == null) return null;
            else return new Library
            {
                Id = dbLibrary.Id,
                Name = dbLibrary.Name,
                Address = dbLibrary.Address,
                Phone = dbLibrary.Phone,
                City = dbLibrary.CityId
            };
        }

        public static explicit operator LibraryDTO (LibraryDetailed dbLibrary)
        {
            if (dbLibrary == null) return null;
            else return new LibraryDTO
            {
                Id = dbLibrary.LibId,
                Name = dbLibrary.LibName,
                Address = dbLibrary.LibAddress,
                Phone = dbLibrary.LibPhone,
                CityId = dbLibrary.CityId,
                CityName = dbLibrary.CityName
            };
        }
    }
}
