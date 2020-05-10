using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class EditLibraryModel
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public List<CityModel> Cities { get; set; }

        public EditLibraryModel()
        {
            Cities = new List<CityModel>();
        }

        public static explicit operator EditLibraryModel(LibraryDTO dbLibrary)
        {
            if (dbLibrary == null) return null;
            else return new EditLibraryModel
            {
                Id = dbLibrary.Id,
                Name = dbLibrary.Name,
                Address = dbLibrary.Address,
                Phone = dbLibrary.Phone,
                CityId = dbLibrary.CityId,
                Cities = new List<CityModel>()
            };
        }
    
        public static explicit operator LibraryDTO (EditLibraryModel dbLibrary)
        {
            if (dbLibrary == null) return null;
            else return new LibraryDTO
            {
                Id = dbLibrary.Id,
                Name = dbLibrary.Name,
                Address = dbLibrary.Address,
                Phone = dbLibrary.Phone,
                CityId = dbLibrary.CityId
            };
        }
    }
}