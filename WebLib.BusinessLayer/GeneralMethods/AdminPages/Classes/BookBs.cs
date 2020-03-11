using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class BookBs : IDbModel<BookDTO>
	{
		private LibDbContext context;

		private GenericRepository<Book> repository;

		public BookBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Book>(context);
		}

		void IDbModel<BookDTO>.Add (BookDTO model)
		{
			if (model != null)
			{
				repository.Create((Book)model);
			}
		}

		void IDbModel<BookDTO>.Delete (int id)
		{
			Book entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		BookDTO IDbModel<BookDTO>.GetById (int id)
		{
			BookDTO entity = (BookDTO)repository.FindById(id);

			return entity;
		}

		List<BookDTO> IDbModel<BookDTO>.GetList ()
		{
			List<BookDTO> entities = repository.Get().Select(c => (BookDTO)c).ToList();

			return entities;
		}

		void IDbModel<BookDTO>.Update (BookDTO model)
		{
			if (model != null)
			{
				Book entity = (Book)model;
				repository.Update(entity);
			}
		}
	}
}
