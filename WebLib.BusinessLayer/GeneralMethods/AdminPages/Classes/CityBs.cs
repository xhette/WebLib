using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class CityBs : IDbModel<CityDTO>
	{
		private LibDbContext context;

		private GenericRepository<City> repository;

		public CityBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<City>(context);
		}

		void IDbModel<CityDTO>.Add (CityDTO model)
		{
			if (model != null)
			{
				repository.Create((City)model);
			}
		}

		void IDbModel<CityDTO>.Delete (int id)
		{
			City entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		CityDTO IDbModel<CityDTO>.GetById (int id)
		{
			CityDTO entity = (CityDTO)repository.FindById(id);

			return entity;
		}

		List<CityDTO> IDbModel<CityDTO>.GetList ()
		{
			List<CityDTO> entities = repository.Get().Select(c => (CityDTO)c).ToList();

			return entities;
		}

		void IDbModel<CityDTO>.Update (CityDTO model)
		{
			if (model != null)
			{
				City entity = (City)model;
				repository.Update(entity);
			}
		}
	}
}
