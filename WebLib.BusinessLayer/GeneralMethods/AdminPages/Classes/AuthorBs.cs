using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class AuthorBs : IDbModel<AuthorDTO>
	{
		private LibContext context;

		private GenericRepository<Authors> repository;

		public AuthorBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Authors>(context);
		}

		public void Add (AuthorDTO model)
		{
			if (model != null)
			{
				repository.Create((Authors)model);
			}
		}

		public void Delete (int id)
		{
			Authors entity = repository.FindById(id);

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
				Authors entity = (Authors)model;
				repository.Update(entity);
			}
		}
	}
}
