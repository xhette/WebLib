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
	public class IssueBs : IDbModel<IssueDTO>
	{
		private LibContext context;

		private GenericRepository<Issues> repository;

		public IssueBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Issues>(context);
		}

		public ResultModel Add(IssueDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Issues)model);
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
				Issues entity = repository.FindById(id);

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

		public IssueDTO GetById(int id)
		{
			IssueDTO entity = (IssueDTO)repository.FindById(id);

			return entity;
		}

		public List<IssueDTO> GetList()
		{
			List<IssueDTO> entities = repository.Get().Select(c => (IssueDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(IssueDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Issues entity = (Issues)model;
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
