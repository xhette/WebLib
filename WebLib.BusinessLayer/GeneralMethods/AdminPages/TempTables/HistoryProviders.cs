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
	public class HistoryProviders : IHistory
	{
		public int Undone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<ProvidersHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.ProvidersHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Providers> generic = new GenericRepository<Providers>(context);

						if (step < history.Count && step >= 0)
						{
							ProvidersHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER ProvidersHistory ON Providers");
							context.Database.ExecuteSqlCommand("DISABLE TRIGGER ProvidersInsert ON Providers");

							if (operation == "inserted")
							{
								Providers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Providers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Surname = pacient.HistorySurname;
									entity.Name = pacient.HistoryName;
									entity.Patronymic = pacient.HistoryPatronymic;
									entity.Address = pacient.HistoryAddress;
									entity.UserId = pacient.HistoryUserId;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Providers entity = new Providers
								{
									Id = pacient.Id,
									Surname = pacient.HistorySurname,
									Name = pacient.HistoryName,
									Patronymic = pacient.HistoryPatronymic,
									Address = pacient.HistoryAddress,
									UserId = pacient.HistoryUserId
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Providers.Add(entity);
									context.SaveChanges();
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariansHistory ON Librarians");
							context.Database.ExecuteSqlCommand("ENABLE TRIGGER ProvidersInsert ON Providers");

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

		public int Redone(int current, DateTime time)
		{
			int step = current;

			try
			{
				step--;

				List<ProvidersHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.ProvidersHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Providers> generic = new GenericRepository<Providers>(context);

					if (step < history.Count && step >= 0)
					{
						ProvidersHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER ProvidersHistory ON Providers");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER ProvidersInsert ON Providers");

						if (operation == "inserted")
						{
							Providers entity = new Providers
							{
								Id = pacient.Id,
								Surname = pacient.CurrentSurname,
								Name = pacient.CurrentName,
								Patronymic = pacient.CurrentPatronymic,
								Address = pacient.CurrentAddress,
								UserId = pacient.CurrentUserId
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Providers.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Providers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Surname = pacient.CurrentSurname;
								entity.Name = pacient.CurrentName;
								entity.Patronymic = pacient.CurrentPatronymic;
								entity.Address = pacient.CurrentAddress;
								entity.UserId = pacient.CurrentUserId;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Providers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER ProvidersHistory ON Providers");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER ProvidersInsert ON Providers");

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

