using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class LibrarianBs : IDbModel<LibrarianDataDTO>
	{
		private LibDbContext context;

		private GenericRepository<Librarian> repository;

		public LibrarianBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Librarian>(context);
		}

		public void Add (LibrarianDataDTO model)
		{
			if (model != null)
			{
				repository.Create((Librarian)model);
			}
		}

		public void Delete (int id)
		{
			Librarian entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public LibrarianDataDTO GetById (int id)
		{
			LibrarianDataDTO entity = (LibrarianDataDTO)repository.FindById(id);

			return entity;
		}

		public List<LibrarianDataDTO> GetList ()
		{
			List<LibrarianDataDTO> entities = repository.Get().Select(c => (LibrarianDataDTO)c).ToList();

			return entities;
		}

		public void Update (LibrarianDataDTO model)
		{
			if (model != null)
			{
				Librarian entity = (Librarian)model;
				repository.Update(entity);
			}
		}
	}
}
