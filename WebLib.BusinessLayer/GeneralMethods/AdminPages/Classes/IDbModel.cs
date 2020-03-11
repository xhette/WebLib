using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	interface IDbModel<T> where T : class
	{
		List<T> GetList ();

		T GetById (int id);

		void Add (T model);

		void Update (T model);

		void Delete (int id);
	}
}
