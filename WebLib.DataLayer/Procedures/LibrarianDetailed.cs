using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.DataLayer.Procedures
{
	public class LibrarianDetailed
	{
        public int LibrarianId { get; set; }

        public string LibrarianSurname { get; set; }

        public string LibrarianName { get; set; }

        public string LibrarianPatronymic { get; set; }

        public string LibrarianAddress { get; set; }

        public string LibrarianPhone { get; set; }

        public int UserId { get; set; }

        public int LibraryId { get; set; }

        public string LibraryName { get; set; }
    }
}
