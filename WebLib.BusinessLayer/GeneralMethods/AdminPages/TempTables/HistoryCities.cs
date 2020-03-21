using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables
{
	public class HistoryCities : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<CitiesHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.CitiesHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Cities> generic = new GenericRepository<Cities>(context);

						if (step < history.Count && step >= 0)
						{
							CitiesHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER CitiesHistory ON Cities");

							if (operation == "inserted")
							{
								Cities entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Cities entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Name = pacient.HistoryName;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Cities entity = new Cities
								{
									Id = pacient.Id,
									Name = pacient.HistoryName
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Cities ON");
									context.Cities.Add(entity);
									context.SaveChanges();
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Cities OFF");
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER CitiesHistory ON Cities");

						}

					}
					step++;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
				return step;
			}
		}

		public int Undone(int current, DateTime time)
		{
			int step = current;

			try
			{
				step--;

				List<CitiesHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.CitiesHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Cities> generic = new GenericRepository<Cities>(context);

					if (step < history.Count && step >= 0)
					{
						CitiesHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER CitiesHistory ON Cities");

						if (operation == "inserted")
						{
							Cities entity = new Cities
							{
								Id = pacient.Id,
								Name = pacient.CurrentName
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Cities.Add(entity);
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Cities ON");
								context.SaveChanges();
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Cities OFF");
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Cities entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Name = pacient.CurrentName;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Cities entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER CitiesHistory ON Cities");

					}

				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return step;
		}
	}
}
