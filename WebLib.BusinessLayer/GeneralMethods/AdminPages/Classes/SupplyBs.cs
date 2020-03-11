using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class SupplyBs : IDbModel<SupplyDTO>
	{
		private LibDbContext context;

		private GenericRepository<Supply> repository;

		public SupplyBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Supply>(context);
		}

		public void Add (SupplyDTO model)
		{
			if (model != null)
			{
				repository.Create((Supply)model);
			}
		}

		public void Delete (int id)
		{
			Supply entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public SupplyDTO GetById (int id)
		{
			SupplyDTO entity = (SupplyDTO)repository.FindById(id);

			return entity;
		}

		public List<SupplyDTO> GetList ()
		{
			List<SupplyDTO> entities = repository.Get().Select(c => (SupplyDTO)c).ToList();

			return entities;
		}

		public void Update (SupplyDTO model)
		{
			if (model != null)
			{
				Supply entity = (Supply)model;
				repository.Update(entity);
			}
		}
	}
}
