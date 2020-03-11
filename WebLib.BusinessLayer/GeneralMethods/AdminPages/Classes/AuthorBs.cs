using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class AuthorBs : IDbModel<AuthorDTO>
	{
		private LibDbContext context;

		private GenericRepository<Author> repository;

		public AuthorBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Author>(context);
		}

		public void Add (AuthorDTO model)
		{
			if (model != null)
			{
				repository.Create((Author)model);
			}
		}

		public void Delete (int id)
		{
			Author entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public AuthorDTO GetById (int id)
		{
			AuthorDTO entity = (AuthorDTO)repository.FindById(id);

			return entity;
		}

		public List<AuthorDTO> GetList ()
		{
			List<AuthorDTO> entities = repository.Get().Select(c => (AuthorDTO)c).ToList();

			return entities;
		}

		public void Update (AuthorDTO model)
		{
			if (model != null)
			{
				Author entity = (Author)model;
				repository.Update(entity);
			}
		}
	}
}
