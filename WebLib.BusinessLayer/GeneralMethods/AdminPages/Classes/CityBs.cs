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
	public class CityBs : IDbModel<CityDTO>
	{
		private LibContext context;

		private GenericRepository<Cities> repository;

		public CityBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Cities>(context);
		}

		public ResultModel Add(CityDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Cities)model);
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
				Cities entity = repository.FindById(id);

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

		public CityDTO GetById(int id)
		{
			CityDTO entity = (CityDTO)repository.FindById(id);

			return entity;
		}

		public List<CityDTO> GetList()
		{
			List<CityDTO> entities = repository.Get().Select(c => (CityDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(CityDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Cities entity = (Cities)model;
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
