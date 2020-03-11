using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class ReaderBs : IDbModel<ReaderDataDTO>
	{
		private LibDbContext context;

		private GenericRepository<Reader> repository;

		public ReaderBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Reader>(context);
		}

		public void Add (ReaderDataDTO model)
		{
			if (model != null)
			{
				repository.Create((Reader)model);
			}
		}

		public void Delete (int id)
		{
			Reader entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public ReaderDataDTO GetById (int id)
		{
			ReaderDataDTO entity = (ReaderDataDTO)repository.FindById(id);

			return entity;
		}

		public List<ReaderDataDTO> GetList ()
		{
			List<ReaderDataDTO> entities = repository.Get().Select(c => (ReaderDataDTO)c).ToList();

			return entities;
		}

		public void Update (ReaderDataDTO model)
		{
			if (model != null)
			{
				Reader entity = (Reader)model;
				repository.Update(entity);
			}
		}
	}
}
