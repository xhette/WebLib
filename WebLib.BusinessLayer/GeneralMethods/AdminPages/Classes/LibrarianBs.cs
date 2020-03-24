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
	public class LibrarianBs : IDbModel<LibrarianDataDTO>
	{
		private LibContext context;

		private GenericRepository<Librarians> repository;

		public LibrarianBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Librarians>(context);
		}

		public ResultModel Add(LibrarianDataDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Librarians)model);
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
				Librarians entity = repository.FindById(id);

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

		public LibrarianDataDTO GetById(int id)
		{
			LibrarianDataDTO entity = (LibrarianDataDTO)repository.FindById(id);

			return entity;
		}

		public List<LibrarianDataDTO> GetList()
		{
			List<LibrarianDataDTO> entities = repository.Get().Select(c => (LibrarianDataDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(LibrarianDataDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Librarians entity = (Librarians)model;
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
