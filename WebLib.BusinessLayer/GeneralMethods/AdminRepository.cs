using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.BusinessLayer.BusinessModels;
using WebLib.BusinessLayer.DTO;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods
{
	class AdminRepository
	{
		LibContext _context;

		public AdminRepository(LibContext context)
		{
			_context = context;
		}

		public ResultModel AddAuthor(AuthorDTO author)
		{
			ResultModel result = new ResultModel();

			try
			{
				Authors item = (Authors)author;
				_context.Authors.Add(item);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = ex.StackTrace;
			}

			return result;
		}
	}
}
