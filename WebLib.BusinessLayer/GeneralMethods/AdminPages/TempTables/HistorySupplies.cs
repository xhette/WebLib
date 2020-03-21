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
	public class HistorySupplies : IHistory
	{
		public int Redone(int current, DateTime time)
		{
			{
				int step = current;

				try
				{
					List<SuppliesHistory> history;

					using (LibContext context = new LibContext())
					{
						history = context.SuppliesHistory.Where(c => c.OperationDate <= time).ToList();
						history.Reverse();
						GenericRepository<Supplies> generic = new GenericRepository<Supplies>(context);

						if (step < history.Count && step >= 0)
						{
							SuppliesHistory pacient = history[step];
							string operation = pacient.Operation;

							context.Database.ExecuteSqlCommand("DISABLE TRIGGER SuppliesHistory ON Supplies");

							if (operation == "inserted")
							{
								Supplies entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									generic.Remove(entity);
								}
							}
							else if (operation == "updated")
							{
								Supplies entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
								if (entity != null)
								{
									entity.Shop = pacient.HistoryShop.Value;
									entity.Summ = pacient.HistorySumm;
									entity.Date = pacient.HistoryDate;

									generic.Update(entity);
								}
							}
							else if (operation == "deleted")
							{
								Supplies entity = new Supplies
								{
									Id = pacient.Id,
									Shop = pacient.HistoryShop.Value,
									Summ = pacient.HistorySumm,
									Date = pacient.HistoryDate,
							};

								using (var scope = context.Database.BeginTransaction())
								{
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Supplies ON");
									context.Supplies.Add(entity);
									context.SaveChanges();
									context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Supplies OFF");
									scope.Commit();
								}
							}

							context.Database.ExecuteSqlCommand("ENABLE TRIGGER SuppliesHistory ON Supplies");

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

				List<SuppliesHistory> history;

				using (LibContext context = new LibContext())
				{
					history = context.SuppliesHistory.Where(c => c.OperationDate <= time).ToList();
					history.Reverse();
					GenericRepository<Supplies> generic = new GenericRepository<Supplies>(context);

					if (step < history.Count && step >= 0)
					{
						SuppliesHistory pacient = history[step];
						string operation = pacient.Operation;

						context.Database.ExecuteSqlCommand("DISABLE TRIGGER SuppliesHistory ON Supplies");

						if (operation == "inserted")
						{
							Supplies entity = new Supplies
							{
								Id = pacient.Id,
								Shop = pacient.CurrentShop.Value,
								Summ = pacient.CurrentSumm,
								Date = pacient.CurrentDate
						};

							using (var scope = context.Database.BeginTransaction())
							{
								context.Supplies.Add(entity);
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Supplies ON");
								context.SaveChanges();
								context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Supplies OFF");
								scope.Commit();
							}
						}
						else if (operation == "updated")
						{
							Supplies entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								entity.Shop = pacient.CurrentShop.Value;
								entity.Summ = pacient.CurrentSumm;
								entity.Date = pacient.CurrentDate;

								generic.Update(entity);
							}
						}
						else if (operation == "deleted")
						{

							Supplies entity = generic.Get(c => c.Id == pacient.Id).FirstOrDefault();
							if (entity != null)
							{
								generic.Remove(entity);
							}
						}

						context.Database.ExecuteSqlCommand("ENABLE TRIGGER SuppliesHistory ON Supplies");

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
