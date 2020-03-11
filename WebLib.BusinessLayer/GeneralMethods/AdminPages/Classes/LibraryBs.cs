using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class LibraryBs : IDbModel<LibraryDTO>
	{
		private LibDbContext context;

		private GenericRepository<Library> repository;

		public LibraryBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Library>(context);
		}

		public void Add (LibraryDTO model)
		{
			if (model != null)
			{
				repository.Create((Library)model);
			}
		}

		public void Delete (int id)
		{
			Library entity = repository.FindById(id);

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
				Library entity = (Library)model;
				repository.Update(entity);
			}
		}
	}
}
