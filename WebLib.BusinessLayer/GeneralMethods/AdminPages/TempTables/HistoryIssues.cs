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
	public class HistoryIssues : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<IssuesHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.IssuesHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Issues> generic = new GenericRepository<Issues>(context);

						if (step < history.Count && step >= 0)
						{
							IssuesHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER IssuesHistory ON Issues");
							context.Database.ExecuteSqlCommand("DISABLE TRIGGER IssuesInsert ON Issues");

							if (operation == "inserted")
							{
								Issues entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Issues entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Book = pacient.HistoryBook.Value;
									entity.Reader = pacient.HistoryReader.Value;
									entity.IssueDate = pacient.HistoryIssueDate;
									entity.ReturnDate = pacient.HistoryReturnDate;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Issues entity = new Issues
								{
									Id = pacient.Id,
									Book = pacient.HistoryBook.Value,
									Reader = pacient.HistoryReader.Value,
									IssueDate = pacient.HistoryIssueDate,
									ReturnDate = pacient.HistoryReturnDate
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Issues.Add(entity);
									context.SaveChanges();
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER IssuesHistory ON Issues");
							context.Database.ExecuteSqlCommand("ENABLE TRIGGER IssuesInsert ON Issues");

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

				List<IssuesHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.IssuesHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Issues> generic = new GenericRepository<Issues>(context);

					if (step < history.Count && step >= 0)
					{
						IssuesHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER IssuesHistory ON Issues");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER IssuesInsert ON Issues");

						if (operation == "inserted")
						{
							Issues entity = new Issues
							{
								Id = pacient.Id,
								Book = pacient.CurrentBook.Value,
								Reader = pacient.CurrentReader.Value,
								IssueDate = pacient.CurrentIssueDate,
								ReturnDate = pacient.CurrentReturnDate
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Issues.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Issues entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Book = pacient.CurrentBook.Value;
								entity.Reader = pacient.CurrentReader.Value;
								entity.IssueDate = pacient.CurrentIssueDate;
								entity.ReturnDate = pacient.CurrentReturnDate;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Issues entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER IssuesHistory ON Issues");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER IssuesInsert ON Issues");

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
