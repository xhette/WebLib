using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class ShopBs : IDbModel<ShopDTO>
	{
		private LibContext context;

		private GenericRepository<Shops> repository;

		public ShopBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Shops>(context);
		}

		public void Add (ShopDTO model)
		{
			if (model != null)
			{
				repository.Create((Shops)model);
			}
		}

		public void Delete (int id)
		{
			Shops entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public ShopDTO GetById (int id)
		{
			ShopDTO entity = (ShopDTO)repository.FindById(id);

			return entity;
		}

		public List<ShopDTO> GetList ()
		{
			List<ShopDTO> entities = repository.Get().Select(c => (ShopDTO)c).ToList();

			return entities;
		}

		public void Update (ShopDTO model)
		{
			if (model != null)
			{
				Shops entity = (Shops)model;
				repository.Update(entity);
			}
		}
	}
}
