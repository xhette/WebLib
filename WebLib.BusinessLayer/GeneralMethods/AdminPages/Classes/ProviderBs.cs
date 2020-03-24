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
	public class ProviderBs : IDbModel<ProviderDataDTO>
	{
		private LibContext context;

		private GenericRepository<Providers> repository;

		public ProviderBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Providers>(context);
		}

		public ResultModel Add(ProviderDataDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Providers)model);
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
				Providers entity = repository.FindById(id);

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

		public ProviderDataDTO GetById(int id)
		{
			ProviderDataDTO entity = (ProviderDataDTO)repository.FindById(id);

			return entity;
		}

		public List<ProviderDataDTO> GetList()
		{
			List<ProviderDataDTO> entities = repository.Get().Select(c => (ProviderDataDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(ProviderDataDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Providers entity = (Providers)model;
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
