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
	public class HistoryDepartments : IHistory
	{

		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<DepartmentsHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.DepartmentsHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Departments> generic = new GenericRepository<Departments>(context);

						if (step < history.Count && step >= 0)
						{
							DepartmentsHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER DepartmentsHistory ON Departments");
							context.Database.ExecuteSqlCommand("DISABLE TRIGGER DepartmentsInsert ON Departments");

							if (operation == "inserted")
							{
								Departments entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Departments entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Name = pacient.HistoryName;
									entity.Library = pacient.HistoryLibrary.HasValue ? pacient.HistoryLibrary.Value : 0;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Departments entity = new Departments
								{
									Id = pacient.Id,
									Name = pacient.HistoryName,
									Library = pacient.HistoryLibrary.HasValue ? pacient.HistoryLibrary.Value : 0
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Departments.Add(entity);
									context.SaveChanges();
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER DepartmentsHistory ON Departments");
							context.Database.ExecuteSqlCommand("ENABLE TRIGGER DepartmentsInsert ON Departments");

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

				List<DepartmentsHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.DepartmentsHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Departments> generic = new GenericRepository<Departments>(context);

					if (step < history.Count && step >= 0)
					{
						DepartmentsHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER DepartmentsHistory ON Departments");
						context.Database.ExecuteSqlCommand("DISABLE TRIGGER DepartmentsInsert ON Departments");

						if (operation == "inserted")
						{
							Departments entity = new Departments
							{
								Id = pacient.Id,
								Name = pacient.CurrentName,
								Library = pacient.CurrentLibrary.HasValue ? pacient.CurrentLibrary.Value : 0
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Departments.Add(entity);
								context.SaveChanges();
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Departments entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Name = pacient.CurrentName;
								entity.Library = pacient.CurrentLibrary.HasValue ? pacient.CurrentLibrary.Value : 0;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Departments entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER DepartmentsHistory ON Departments");
						context.Database.ExecuteSqlCommand("ENABLE TRIGGER DepartmentsInsert ON Departments");

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
