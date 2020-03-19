using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class IssueBs : IDbModel<IssueDTO>
	{
		private LibContext context;

		private GenericRepository<Issues> repository;

		public IssueBs ()
		{
			context = new LibContext();
			repository = new GenericRepository<Issues>(context);
		}

		public void Add (IssueDTO model)
		{
			if (model != null)
			{
				repository.Create((Issues)model);
			}
		}

		public void Delete (int id)
		{
			Issues entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		public IssueDTO GetById (int id)
		{
			IssueDTO entity = (IssueDTO)repository.FindById(id);

			return entity;
		}

		public List<IssueDTO> GetList ()
		{
			List<IssueDTO> entities = repository.Get().Select(c => (IssueDTO)c).ToList();

			return entities;
		}

		public void Update (IssueDTO model)
		{
			if (model != null)
			{
				Issues entity = (Issues)model;
				repository.Update(entity);
			}
		}
	}
}
