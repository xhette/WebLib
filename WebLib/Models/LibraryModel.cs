using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
    public class LibraryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public static explicit operator LibraryModel (LibraryDTO dbLibrary)
        {
            if (dbLibrary == null) return null;
            else return new LibraryModel
            {
                Id = dbLibrary.Id,
                Name = dbLibrary.Name,
                Address = dbLibrary.Address,
                Phone = dbLibrary.Phone,
                CityId = dbLibrary.CityId,
                CityName = dbLibrary.CityName
            };
        }
    }
}