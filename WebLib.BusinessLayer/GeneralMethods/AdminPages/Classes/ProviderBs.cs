using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class ProviderBs : IDbModel<ProviderDataDTO>
	{
		private LibContext context;

		private GenericRepository<Providers> repository;

		public ProviderBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Providers>(context);
		}

		public void Add (ProviderDataDTO model)
		{
			if (model != null)
			{
				repository.Create((Providers)model);
			}
		}

		public void Delete (int id)
		{
			Providers entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public ProviderDataDTO GetById (int id)
		{
			ProviderDataDTO entity = (ProviderDataDTO)repository.FindById(id);

			return entity;
		}

		public List<ProviderDataDTO> GetList ()
		{
			List<ProviderDataDTO> entities = repository.Get().Select(c => (ProviderDataDTO)c).ToList();

			return entities;
		}

		public void Update (ProviderDataDTO model)
		{
			if (model != null)
			{
				Providers entity = (Providers)model;
				repository.Update(entity);
			}
		}
	}
}
