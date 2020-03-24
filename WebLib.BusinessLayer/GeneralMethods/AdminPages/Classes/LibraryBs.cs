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
	public class LibraryBs : IDbModel<LibraryDTO>
	{
		private LibContext context;

		private GenericRepository<Libraries> repository;

		public LibraryBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Libraries>(context);
		}

		public ResultModel Add(LibraryDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Libraries)model);
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
				Libraries entity = repository.FindById(id);

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

		public LibraryDTO GetById(int id)
		{
			LibraryDTO entity = (LibraryDTO)repository.FindById(id);

			return entity;
		}

		public List<LibraryDTO> GetList()
		{
			List<LibraryDTO> entities = repository.Get().Select(c => (LibraryDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(LibraryDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Libraries entity = (Libraries)model;
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
