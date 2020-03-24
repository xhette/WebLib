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
	public class ReaderBs : IDbModel<ReaderDataDTO>
	{
		private LibContext context;

		private GenericRepository<Readers> repository;

		public ReaderBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Readers>(context);
		}

		public ResultModel Add(ReaderDataDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Readers)model);
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
				Readers entity = repository.FindById(id);

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

		public ReaderDataDTO GetById(int id)
		{
			ReaderDataDTO entity = (ReaderDataDTO)repository.FindById(id);

			return entity;
		}

		public List<ReaderDataDTO> GetList()
		{
			List<ReaderDataDTO> entities = repository.Get().Select(c => (ReaderDataDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(ReaderDataDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Readers entity = (Readers)model;
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
