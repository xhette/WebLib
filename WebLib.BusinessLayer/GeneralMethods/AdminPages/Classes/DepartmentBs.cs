using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class DepartmentBs : IDbModel<DepartmentDTO>
	{
		private LibDbContext context;

		private GenericRepository<Department> repository;

		public DepartmentBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Department>(context);
		}

		void IDbModel<DepartmentDTO>.Add (DepartmentDTO model)
		{
			if (model != null)
			{
				repository.Create((Department)model);
			}
		}

		void IDbModel<DepartmentDTO>.Delete (int id)
		{
			Department entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		DepartmentDTO IDbModel<DepartmentDTO>.GetById (int id)
		{
			DepartmentDTO entity = (DepartmentDTO)repository.FindById(id);

			return entity;
		}

		List<DepartmentDTO> IDbModel<DepartmentDTO>.GetList ()
		{
			List<DepartmentDTO> entities = repository.Get().Select(c => (DepartmentDTO)c).ToList();

			return entities;
		}

		void IDbModel<DepartmentDTO>.Update (DepartmentDTO model)
		{
			if (model != null)
			{
				Department entity = (Department)model;
				repository.Update(entity);
			}
		}
	}
}
