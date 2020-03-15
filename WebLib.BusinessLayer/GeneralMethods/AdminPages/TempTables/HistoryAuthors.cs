using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables
{
	public static class HistoryAuthors
	{
		public static int Undone(int current, DateTime time)
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

					if (step <= history.Count)
					{
						AuthorsHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER AuthorsHistory ON Authors");

						if (operation == "inserted")
						{
							Authors author = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							generic.Remove(author);
						}
						else if (operation == "updated")
						{
							Authors author = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							author.Surname = pacient.HistorySurname;
							author.Name = pacient.HistoryName;
							author.Patronymic = pacient.HistoryPatronymic;

							generic.Update(author);
						}
						else if (operation == "deleted")
						{
							context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Authors ON");
							Authors author = new Authors
							{
								Id = pacient.Id,
								Surname = pacient.HistorySurname,
								Name = pacient.HistoryName,
								Patronymic = pacient.HistoryPatronymic
							};
							generic.Create(author);
							context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Authors OFF");
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER AuthorsHistory ON Authors");

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

		public static int Redone(int current, DateTime time)
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

					if (step > 0)
					{
						step--;

						AuthorsHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER AuthorsHistory ON Authors");

						if (operation == "inserted")
						{
							context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Authors ON");
							Authors author = new Authors
							{
								Id = pacient.Id,
								Surname = pacient.CurrentSurname,
								Name = pacient.CurrentName,
								Patronymic = pacient.CurrentPatronymic
							};
							generic.Create(author);
							context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Authors OFF");
						}
						else if (operation == "updated")
						{
							Authors author = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							author.Surname = pacient.CurrentSurname;
							author.Name = pacient.CurrentName;
							author.Patronymic = pacient.CurrentPatronymic;

							generic.Update(author);
						}
						else if (operation == "deleted")
						{

							Authors author = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							generic.Remove(author);
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER AuthorsHistory ON Authors");

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
