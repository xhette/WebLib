using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class SupplyBs : IDbModel<SupplyDTO>
	{
		private LibContext context;

		private GenericRepository<Supplies> repository;

		public SupplyBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Supplies>(context);
		}

		public void Add (SupplyDTO model)
		{
			if (model != null)
			{
				repository.Create((Supplies)model);
			}
		}

		public void Delete (int id)
		{
			Supplies entity = repository.FindById(id);

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
				Supplies entity = (Supplies)model;
				repository.Update(entity);
			}
		}
	}
}
