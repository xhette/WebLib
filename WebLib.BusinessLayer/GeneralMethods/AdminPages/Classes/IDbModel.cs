using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.BusinessLayer.BusinessModels;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	interface IDbModel<T> where T : class
	{
		List<T> GetList ();

		T GetById (int id);

		ResultModel Add (T model);

		ResultModel Update (T model);

		ResultModel Delete (int id);
	}
}
