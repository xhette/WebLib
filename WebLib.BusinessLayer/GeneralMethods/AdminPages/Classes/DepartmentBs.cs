using System;
using System.Collections.Generic;
using System.Linq;
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

		public DepartmentBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Departments>(context);
		}

		public void Add (DepartmentDTO model)
		{
			if (model != null)
			{
				repository.Create((Departments)model);
			}
		}

		public void Delete (int id)
		{
			Departments entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public DepartmentDTO GetById (int id)
		{
			DepartmentDTO entity = (DepartmentDTO)repository.FindById(id);

			return entity;
		}

		public List<DepartmentDTO> GetList ()
		{
			List<DepartmentDTO> entities = repository.Get().Select(c => (DepartmentDTO)c).ToList();

			return entities;
		}

		public void Update (DepartmentDTO model)
		{
			if (model != null)
			{
				Departments entity = (Departments)model;
				repository.Update(entity);
			}
		}
	}
}
