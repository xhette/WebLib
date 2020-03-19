using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class ReaderBs : IDbModel<ReaderDataDTO>
	{
		private LibContext context;

		private GenericRepository<Readers> repository;

		public ReaderBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Readers>(context);
		}

		public void Add (ReaderDataDTO model)
		{
			if (model != null)
			{
				repository.Create((Readers)model);
			}
		}

		public void Delete (int id)
		{
			Readers entity = repository.FindById(id);

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
				Readers entity = (Readers)model;
				repository.Update(entity);
			}
		}
	}
}
