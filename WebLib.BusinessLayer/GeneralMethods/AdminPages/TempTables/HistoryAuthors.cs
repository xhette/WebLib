using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables
{
	public class HistoryAuthors : IHistory 
	{
		public int Undone(int current, DateTime time)
		{
			int step = current;

			try
			{
				List<AuthorsHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.AuthorsHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Authors> generic = new GenericRepository<Authors>(context);

					if (step < history.Count && step >= 0)
					{
						AuthorsHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER AuthorsHistory ON Authors");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER AuthorsInsert ON Authors");

						if (operation == "inserted")
						{
							Authors entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}
						else if (operation == "updated")
						{
							Authors entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Surname = pacient.HistorySurname;
								entity.Name = pacient.HistoryName;
								entity.Patronymic = pacient.HistoryPatronymic;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{
							Authors entity = new Authors
							{
								Id = pacient.Id,
								Surname = pacient.HistorySurname,
								Name = pacient.HistoryName,
								Patronymic = pacient.HistoryPatronymic
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Authors.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER AuthorsHistory ON Authors");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER AuthorsInsert ON Authors");

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

		public int Redone(int current, DateTime time)
		{
			int step = current;

			try
			{
				step--;

				List<AuthorsHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.AuthorsHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Authors> generic = new GenericRepository<Authors>(context);

					if (step < history.Count && step >= 0)
					{
						AuthorsHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER AuthorsHistory ON Authors");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER AuthorsInsert ON Authors");

						if (operation == "inserted")
						{
							Authors entity = new Authors
							{
								Id = pacient.Id,
								Surname = pacient.CurrentSurname,
								Name = pacient.CurrentName,
								Patronymic = pacient.CurrentPatronymic
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Authors.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Authors entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Surname = pacient.CurrentSurname;
								entity.Name = pacient.CurrentName;
								entity.Patronymic = pacient.CurrentPatronymic;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Authors entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER AuthorsHistory ON Authors");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER AuthorsInsert ON Authors");

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
