using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class ReaderShortDataDTO
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Surname { get; set; }

		public string Name { get; set; }

		public string Patronymic { get; set; }
	}
}
