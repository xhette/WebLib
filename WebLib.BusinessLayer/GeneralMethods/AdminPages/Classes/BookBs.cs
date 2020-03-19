using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class BookBs : IDbModel<BookDTO>
	{
		private LibContext context;

		private GenericRepository<Books> repository;

		public BookBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Books>(context);
		}

		public void Add (BookDTO model)
		{
			if (model != null)
			{
				repository.Create((Books)model);
			}
		}

		public void Delete (int id)
		{
			Books entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public BookDTO GetById (int id)
		{
			BookDTO entity = (BookDTO)repository.FindById(id);

			return entity;
		}

		public List<BookDTO> GetList ()
		{
			List<BookDTO> entities = repository.Get().Select(c => (BookDTO)c).ToList();

			return entities;
		}

		public void Update (BookDTO model)
		{
			if (model != null)
			{
				Books entity = (Books)model;
				repository.Update(entity);
			}
		}
	}
}
