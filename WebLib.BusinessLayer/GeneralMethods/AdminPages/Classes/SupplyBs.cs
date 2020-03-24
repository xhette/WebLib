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
	public class SupplyBs : IDbModel<SupplyDTO>
	{
		private LibContext context;

		private GenericRepository<Supplies> repository;

		public SupplyBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Supplies>(context);
		}

		public ResultModel Add(SupplyDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Supplies)model);
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
				Supplies entity = repository.FindById(id);

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

		public SupplyDTO GetById(int id)
		{
			SupplyDTO entity = (SupplyDTO)repository.FindById(id);

			return entity;
		}

		public List<SupplyDTO> GetList()
		{
			List<SupplyDTO> entities = repository.Get().Select(c => (SupplyDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(SupplyDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Supplies entity = (Supplies)model;
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
