using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class LibrarianBs : IDbModel<LibrarianDataDTO>
	{
		private LibContext context;

		private GenericRepository<Librarians> repository;

		public LibrarianBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Librarians>(context);
		}

		public void Add (LibrarianDataDTO model)
		{
			if (model != null)
			{
				repository.Create((Librarians)model);
			}
		}

		public void Delete (int id)
		{
			Librarians entity = repository.FindById(id);

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
				Librarians entity = (Librarians)model;
				repository.Update(entity);
			}
		}
	}
}
