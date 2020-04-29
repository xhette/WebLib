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
	public class HistoryLibrarians : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<LibrariansHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.LibrariansHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Librarians> generic = new GenericRepository<Librarians>(context);

						if (step < history.Count && step >= 0)
						{
							LibrariansHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariansHistory ON Librarians");
							context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariansInsert ON Librarians");

							if (operation == "inserted")
							{
								Librarians entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Librarians entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Surname = pacient.HistorySurname;
									entity.Name = pacient.HistoryName;
									entity.Patronymic = pacient.HistoryPatronymic;
									entity.Phone = pacient.HistoryPhone;
									entity.Address = pacient.HistoryAddress;
									entity.Library = pacient.HistoryLibrary.Value;
									entity.UserId = pacient.HistoryUserId;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Librarians entity = new Librarians
								{
									Id = pacient.Id,
									Surname = pacient.HistorySurname,
									Name = pacient.HistoryName,
									Patronymic = pacient.HistoryPatronymic,
									Library = pacient.HistoryLibrary.Value,
									Phone = pacient.HistoryPhone,
									Address = pacient.HistoryAddress,
									UserId = pacient.HistoryUserId
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Librarians.Add(entity);
									context.SaveChanges();
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariansHistory ON Librarians");
							context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariansInsert ON Librarians");

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

				List<LibrariansHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.LibrariansHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Librarians> generic = new GenericRepository<Librarians>(context);

					if (step < history.Count && step >= 0)
					{
						LibrariansHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariansHistory ON Librarians");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER LibrariansInsert ON Librarians");

						if (operation == "inserted")
						{
							Librarians entity = new Librarians
							{
								Id = pacient.Id,
								Surname = pacient.CurrentSurname,
								Name = pacient.CurrentName,
								Patronymic = pacient.CurrentPatronymic,
								Library = pacient.CurrentLibrary.Value,
								Phone = pacient.CurrentPhone,
								Address = pacient.CurrentAddress,
								UserId = pacient.CurrentUserId
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Librarians.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Librarians entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Surname = pacient.CurrentSurname;
								entity.Name = pacient.CurrentName;
								entity.Patronymic = pacient.CurrentPatronymic;
								entity.Library = pacient.CurrentLibrary.Value;
								entity.Phone = pacient.CurrentPhone;
								entity.Address = pacient.CurrentAddress;
								entity.UserId = pacient.CurrentUserId;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Librarians entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariansHistory ON Librarians");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER LibrariansInsert ON Librarians");

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
