using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.BusinessLayer.BusinessModels;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods
{
	public class Registration
	{
		public ResultModel RegisterReader(ReaderDataDTO reader)
		{
			ResultModel result = new ResultModel();
			try
			{
				ReaderBs bs = new ReaderBs();
				bs.Add(reader);
				result.Message = "Регистрация завершена успешно";
			}
			catch (Exception ex)
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = ex.StackTrace;
			}

			return result;
		}

		public ResultModel RegisterEmployee (UserTypeEnum type, int userId, int id)
		{
			ResultModel result = new ResultModel();
			try
			{
				using (LibContext context = new LibContext())
				{

					if (type == UserTypeEnum.Librarian)
					{
						GenericRepository<Librarians> generic = new GenericRepository<Librarians>(context);

						var librarian = generic.FindById(id);
						if (librarian != null)
						{
							librarian.UserId = userId;
							generic.Update(librarian);
						}
						else
						{
							result.Code = OperationStatusEnum.UnexpectedError;
							result.Message = "Неверный идентификатор сотрудника";
						}
					}
					else if (type == UserTypeEnum.Provider)
					{
						GenericRepository<Providers> generic = new GenericRepository<Providers>(context);

						var provider = generic.FindById(id);
						if (provider != null)
						{
							provider.UserId = userId;
							generic.Update(provider);
						}
						else
						{
							result.Code = OperationStatusEnum.UnexpectedError;
							result.Message = "Неверный идентификатор сотрудника";
						}
					}
					else
					{
						result.Code = OperationStatusEnum.UnexpectedError;
						result.Message = "Ошибка присваивания роли пользователя";
					}
				}
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
