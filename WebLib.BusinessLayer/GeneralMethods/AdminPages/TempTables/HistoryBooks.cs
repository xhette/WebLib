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
	public class HistoryBooks : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<BooksHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.BooksHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Books> generic = new GenericRepository<Books>(context);

						if (step < history.Count && step >= 0)
						{
							BooksHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER BooksHistory ON Books");
							context.Database.ExecuteSqlCommand("DISABLE TRIGGER BooksInsert ON Books");

							if (operation == "inserted")
							{
								Books entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Books entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Title = pacient.HistoryTitle;
									entity.Author = pacient.HistoryAuthor.HasValue ? pacient.HistoryAuthor.Value : 0;
									entity.Department = pacient.HistoryDepartment;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Books entity = new Books
								{
									Id = pacient.Id,
									Title = pacient.HistoryTitle,
									Department = pacient.HistoryDepartment,
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Books.Add(entity);
									context.SaveChanges();
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER BooksHistory ON Books");
							context.Database.ExecuteSqlCommand("ENABLE TRIGGER BooksInsert ON Books");

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

				List<BooksHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.BooksHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Books> generic = new GenericRepository<Books>(context);

					if (step < history.Count && step >= 0)
					{
						BooksHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER BooksHistory ON Books");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER BooksInsert ON Books");

						if (operation == "inserted")
						{
							Books entity = new Books
							{
								Id = pacient.Id,
								Title = pacient.CurrentTitle,
								Author = pacient.CurrentAuthor.HasValue ? pacient.CurrentAuthor.Value : 0,
								Department = pacient.CurrentDepartment
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Books.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Books entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Title = pacient.CurrentTitle;
								entity.Author = pacient.CurrentAuthor.HasValue ? pacient.CurrentAuthor.Value : 0;
								entity.Department = pacient.CurrentDepartment;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Books entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER BooksHistory ON Books");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER BooksInsert ON Books");

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
