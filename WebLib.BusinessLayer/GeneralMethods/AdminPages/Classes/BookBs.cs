using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.BusinessModels;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class BookBs : IDbModel<BookDTO>
	{
		private LibContext context;

		private GenericRepository<Books> repository;

		public BookBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Books>(context);
		}

		public ResultModel Add (BookDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Books)model);
				}
				catch (Exception ex)
				{
					result.Code = OperationStatusEnum.UnexpectedError;
					result.Message = ex.Message;
				}
			}
			else
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = "Ошибка при добавлении данных";
			}

			return result;
		}

		public ResultModel Delete(int id)
		{
			ResultModel result = new ResultModel();

			try
			{
				Books entity = repository.FindById(id);

				if (entity != null)
				{
					repository.Remove(entity);
				}
			}
			catch (Exception ex)
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = ex.Message;
			}

			return result;

		}

		public BookDTO GetById (int id)
		{
			BookDTO entity = (BookDTO)repository.FindById(id);

			return entity;
		}

		public List<BookDTO> GetList ()
		{
			List<BookDTO> entities = repository.Get().Select(c => (BookDTO)c).ToList();

			return entities;
		}

		public ResultModel Update (BookDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Books entity = (Books)model;
					repository.Update(entity);
				}
			}
			catch (Exception ex)
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = ex.Message;
			}

			return result;
		}
	}
}
