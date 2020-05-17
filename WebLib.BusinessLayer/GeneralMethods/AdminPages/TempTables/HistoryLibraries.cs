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
	public class HistoryLibraries : IHistory
	{
		public int Undone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<LibrariesHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.LibrariesHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Libraries> generic = new GenericRepository<Libraries>(context);

						if (step < history.Count && step >= 0)
						{
							LibrariesHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariesHistory ON Libraries");
							context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariesInsert ON Libraries");

							if (operation == "inserted")
							{
								Libraries entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Libraries entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Name = pacient.HistoryName;
									entity.Phone = pacient.HistoryPhone;
									entity.Address = pacient.HistoryAddress;
									entity.City = pacient.HistoryCity.HasValue ? pacient.HistoryCity.Value : 0;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Libraries entity = new Libraries
								{
									Id = pacient.Id,
									Name = pacient.HistoryName,
									Phone = pacient.HistoryPhone,
									Address = pacient.HistoryAddress,
									City = pacient.HistoryCity.HasValue ? pacient.HistoryCity.Value : 0
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Libraries.Add(entity);
									context.SaveChanges();
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER CitiesHistory ON Cities");
							context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariesInsert ON Libraries");

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

				List<LibrariesHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.LibrariesHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Libraries> generic = new GenericRepository<Libraries>(context);

					if (step < history.Count && step >= 0)
					{
						LibrariesHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariesHistory ON Libraries");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariesInsert ON Libraries");

						if (operation == "inserted")
						{
							Libraries entity = new Libraries
							{
								Id = pacient.Id,
								Name = pacient.CurrentName,
								Phone = pacient.CurrentPhone,
								Address = pacient.CurrentAddress,
								City = pacient.CurrentCity.HasValue ? pacient.CurrentCity.Value : 0
						};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Libraries.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Libraries entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Name = pacient.CurrentName;
								entity.Phone = pacient.CurrentPhone;
								entity.Address = pacient.CurrentAddress;
								entity.City = pacient.CurrentCity.HasValue ? pacient.CurrentCity.Value : 0;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Libraries entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariesHistory ON Libraries");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariesInsert ON Libraries");

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
