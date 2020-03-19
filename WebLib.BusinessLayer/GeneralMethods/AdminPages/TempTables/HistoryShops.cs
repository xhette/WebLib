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
	class HistoryShops : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<ShopsHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.ShopsHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Shops> generic = new GenericRepository<Shops>(context);

						if (step < history.Count && step >= 0)
						{
							ShopsHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER ShopsHistory ON Shops");

							if (operation == "inserted")
							{
								Shops entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Shops entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Name = pacient.HistoryName;
									entity.Phone = pacient.HistoryPhone;
									entity.Address = pacient.HistoryAddress;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Shops entity = new Shops
								{
									Id = pacient.Id,
									Name = pacient.HistoryName,
									Phone = pacient.HistoryPhone,
									Address = pacient.HistoryAddress
								};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Shops ON");
									context.Shops.Add(entity);
									context.SaveChanges();
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Shops OFF");
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER ShopsHistory ON Shops");

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

				List<ShopsHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.ShopsHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Shops> generic = new GenericRepository<Shops>(context);

					if (step < history.Count && step >= 0)
					{
						ShopsHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER ShopsHistory ON Shops");

						if (operation == "inserted")
						{
							Shops entity = new Shops
							{
								Id = pacient.Id,
								Name = pacient.CurrentName,
								Phone = pacient.CurrentPhone,
								Address = pacient.CurrentAddress
							};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Shops.Add(entity);
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Shops ON");
								context.SaveChanges();
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Shops OFF");
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Shops entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Name = pacient.CurrentName;
								entity.Phone = pacient.CurrentPhone;
								entity.Address = pacient.CurrentAddress;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Shops entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER ShopsHistory ON Shops");

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
