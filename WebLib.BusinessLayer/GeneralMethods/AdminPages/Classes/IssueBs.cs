using System;
using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.Classes
{
	public class IssueBs : IDbModel<IssueDTO>
	{
		private LibDbContext context;

		private GenericRepository<Issue> repository;

		public IssueBs ()
		{
			context = new LibDbContext();
			repository = new GenericRepository<Issue>(context);
		}

		void IDbModel<IssueDTO>.Add (IssueDTO model)
		{
			if (model != null)
			{
				repository.Create((Issue)model);
			}
		}

		void IDbModel<IssueDTO>.Delete (int id)
		{
			Issue entity = repository.FindById(id);

			if (entity != null)
			{
				repository.Remove(entity);
			}
		}

		IssueDTO IDbModel<IssueDTO>.GetById (int id)
		{
			IssueDTO entity = (IssueDTO)repository.FindById(id);

			return entity;
		}

		List<IssueDTO> IDbModel<IssueDTO>.GetList ()
		{
			List<IssueDTO> entities = repository.Get().Select(c => (IssueDTO)c).ToList();

			return entities;
		}

		void IDbModel<IssueDTO>.Update (IssueDTO model)
		{
			if (model != null)
			{
				Issue entity = (Issue)model;
				repository.Update(entity);
			}
		}
	}
}
