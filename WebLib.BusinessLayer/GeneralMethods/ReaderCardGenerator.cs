using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.GeneralMethods
{
	public static class ReaderCardGenerator
	{
		public static string Generate(int readerId, int libId)
		{
			string libStr = String.Format("L{0}", libId);
			string readerStr = String.Format("R{0}", readerId);
			string today = DateTime.Now.ToString("ddMMyy");

			return $"AC-{libStr}{readerStr}-{today}";
		}
	}
}
