using System;
using System.Collections.Generic;
using System.Linq;
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

		public LibraryBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Libraries>(context);
		}

		public void Add (LibraryDTO model)
		{
			if (model != null)
			{
				repository.Create((Libraries)model);
			}
		}

		public void Delete (int id)
		{
			Libraries entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public LibraryDTO GetById (int id)
		{
			LibraryDTO entity = (LibraryDTO)repository.FindById(id);

			return entity;
		}

		public List<LibraryDTO> GetList ()
		{
			List<LibraryDTO> entities = repository.Get().Select(c => (LibraryDTO)c).ToList();

			return entities;
		}

		public void Update (LibraryDTO model)
		{
			if (model != null)
			{
				Libraries entity = (Libraries)model;
				repository.Update(entity);
			}
		}
	}
}
