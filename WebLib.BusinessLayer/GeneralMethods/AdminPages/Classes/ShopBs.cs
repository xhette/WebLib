using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	class ShopBs : IDbModel<ShopDTO>
	{
		private LibDbContext context;

		private GenericRepository<Shop> repository;

		public ShopBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Shop>(context);
		}

		public void Add (ShopDTO model)
		{
			if (model != null)
			{
				repository.Create((Shop)model);
			}
		}

		public void Delete (int id)
		{
			Shop entity = repository.FindById(id);

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
				Shop entity = (Shop)model;
				repository.Update(entity);
			}
		}
	}
}
