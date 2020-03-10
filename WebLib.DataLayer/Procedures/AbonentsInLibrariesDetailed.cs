using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.DataLayer.Procedures
{
	public class AbonentsInLibrariesDetailed
	{
		public string ReaderCard { get; set; }

		public int? ReaderId { get; set; }

		public string ReaderSurname { get; set; }

		public string ReaderName { get; set; }

		public string ReaderPatronymic { get; set; }

		public int? AbonentStatus { get; set; }

		public int LibraryId { get; set; }

		public string LibraryName { get; set; }

		public string LibAddress { get; set; }

		public string LibPhone { get; set; }

		public int CityId { get; set; }

		public string CityName { get; set; }
	}
}
