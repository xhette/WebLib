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
	public class DepartmentBs : IDbModel<DepartmentDTO>
	{
		private LibContext context;

		private GenericRepository<Departments> repository;

		public DepartmentBs()
		{
			context = new LibContext();
			repository = new GenericRepository<Departments>(context);
		}

		public ResultModel Add(DepartmentDTO model)
		{
			ResultModel result = new ResultModel();

			if (model != null)
			{
				try
				{
					repository.Create((Departments)model);
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
				Departments entity = repository.FindById(id);

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

		public DepartmentDTO GetById(int id)
		{
			DepartmentDTO entity = (DepartmentDTO)repository.FindById(id);

			return entity;
		}

		public List<DepartmentDTO> GetList()
		{
			List<DepartmentDTO> entities = repository.Get().Select(c => (DepartmentDTO)c).ToList();

			return entities;
		}

		public ResultModel Update(DepartmentDTO model)
		{
			ResultModel result = new ResultModel();

			try
			{
				if (model != null)
				{
					Departments entity = (Departments)model;
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
