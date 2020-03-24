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
	public class ShopBs : IDbModel<ShopDTO>
	{
		private LibContext context;

		private GenericRepository<Shops> repository;

		public ShopBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Shops>(context);
		}

		public ResultModel Add(ShopDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Shops)model);
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
				Shops entity = repository.FindById(id);

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

		public ShopDTO GetById(int id)
		{
			ShopDTO entity = (ShopDTO)repository.FindById(id);

			return entity;
		}

		public List<ShopDTO> GetList()
		{
			List<ShopDTO> entities = repository.Get().Select(c => (ShopDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(ShopDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Shops entity = (Shops)model;
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
