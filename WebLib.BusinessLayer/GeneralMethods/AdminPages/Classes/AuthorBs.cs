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
	public class AuthorBs : IDbModel<AuthorDTO>
	{
		private LibContext context;

		private GenericRepository<Authors> repository;

		public AuthorBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Authors>(context);
		}

		public ResultModel Add(AuthorDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Authors)model);
					result.Message = "Данные успешно добавлены";
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
				Authors entity = repository.FindById(id);

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

		public AuthorDTO GetById(int id)
		{
			AuthorDTO entity = (AuthorDTO)repository.FindById(id);

			return entity;
		}

		public List<AuthorDTO> GetList()
		{
			List<AuthorDTO> entities = repository.Get().Select(c => (AuthorDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(AuthorDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Authors entity = (Authors)model;
					repository.Update(entity);
				}
			}
			catch (Exception ex)
			{
				result.Code = OperationStatusEnum.UnexpectedError;

			}

			return result;
		}
	}
}
