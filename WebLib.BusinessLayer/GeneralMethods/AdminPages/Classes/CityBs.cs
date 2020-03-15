using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class CityBs : IDbModel<CityDTO>
	{
		private LibContext context;

		private GenericRepository<Cities> repository;

		public CityBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Cities>(context);
		}

		void IDbModel<CityDTO>.Add (CityDTO model)
		{
			if (model != null)
			{
				repository.Create((Cities)model);
			}
		}

		void IDbModel<CityDTO>.Delete (int id)
		{
			Cities entity = repository.FindById(id);

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
				Cities entity = (Cities)model;
				repository.Update(entity);
			}
		}
	}
}
