using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class ProviderBs : IDbModel<ProviderDataDTO>
	{
		private LibDbContext context;

		private GenericRepository<Provider> repository;

		public ProviderBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Provider>(context);
		}

		public void Add (ProviderDataDTO model)
		{
			if (model != null)
			{
				repository.Create((Provider)model);
			}
		}

		public void Delete (int id)
		{
			Provider entity = repository.FindById(id);

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
				Provider entity = (Provider)model;
				repository.Update(entity);
			}
		}
	}
}
