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
	class HistoryReaders : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<ReadersHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.ReadersHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Readers> generic = new GenericRepository<Readers>(context);

						if (step < history.Count && step >= 0)
						{
							ReadersHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER ReadersHistory ON Readers");

							if (operation == "inserted")
							{
								Readers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Readers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Surname = pacient.HistorySurname;
									entity.Name = pacient.HistoryName;
									entity.Patronymic = pacient.HistoryPatronymic;
									entity.BirthDate = pacient.HistoryBirthDate;
									entity.PassSeria = pacient.HistoryPassSeria;
									entity.PassNumber = pacient.HistoryPassNumber;
									entity.Phone = pacient.HistoryPhone;
									entity.Address = pacient.HistoryAddress;
									entity.UserId = pacient.HistoryUserId;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Readers entity = new Readers
								{
									Id = pacient.Id,
									Surname = pacient.HistorySurname,
									Name = pacient.HistoryName,
									Patronymic = pacient.HistoryPatronymic,
									BirthDate = pacient.HistoryBirthDate,
									PassSeria = pacient.HistoryPassSeria,
									PassNumber = pacient.HistoryPassNumber,
									Phone = pacient.HistoryPhone,
									Address = pacient.HistoryAddress,
									UserId = pacient.HistoryUserId
							};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Readers ON");
									context.Readers.Add(entity);
									context.SaveChanges();
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Readers OFF");
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER ReadersHistory ON Readers");

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

				List<ReadersHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.ReadersHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Readers> generic = new GenericRepository<Readers>(context);

					if (step < history.Count && step >= 0)
					{
						ReadersHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER ReadersHistory ON Readers");

						if (operation == "inserted")
						{
							Readers entity = new Readers
							{
								Id = pacient.Id,
								Surname = pacient.CurrentSurname,
								Name = pacient.CurrentName,
								Patronymic = pacient.CurrentPatronymic,
								BirthDate = pacient.CurrentBirthDate,
								PassSeria = pacient.CurrentPassSeria,
								PassNumber = pacient.CurrentPassNumber,
								Phone = pacient.CurrentPhone,
								Address = pacient.CurrentAddress,
								UserId = pacient.CurrentUserId
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Readers.Add(entity);
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Readers ON");
								context.SaveChanges();
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Readers OFF");
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Readers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Surname = pacient.CurrentSurname;
								entity.Name = pacient.CurrentName;
								entity.Patronymic = pacient.CurrentPatronymic;
								entity.BirthDate = pacient.CurrentBirthDate;
								entity.PassSeria = pacient.CurrentPassSeria;
								entity.PassNumber = pacient.CurrentPassNumber;
								entity.Phone = pacient.CurrentPhone;
								entity.Address = pacient.CurrentAddress;
								entity.UserId = pacient.CurrentUserId;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Readers entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER ReadersHistory ON Readers");

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
