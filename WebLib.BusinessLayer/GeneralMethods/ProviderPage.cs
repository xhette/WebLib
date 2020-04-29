using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLib.BusinessLayer.BusinessModels;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods
{
	public class ProviderPage
	{
		LibContext _context;

		public ProviderPage(LibContext context)
		{
			_context = context;
		}

		public List<LibraryDTO> LibraryList()
		{
			List<LibraryDTO> result;

			StoredProcedure procedure = new StoredProcedure(_context);

			result = procedure.LibraryDetailedList().Select(c => (LibraryDTO)c).ToList();
			return result;
		}

		public List<CityDTO> CityList()
		{
			GenericRepository<Cities> repository = new GenericRepository<Cities>(_context);
			List<CityDTO> cities = repository.Get().Select(c => (CityDTO)c).ToList();

			return cities;
		}

		public List<BookDetailedDTO> Books()
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<BookDetailedDTO> books = procedure.ProviderBookList().Select(c => (BookDetailedDTO)c).ToList();

			return books;
		}

		public List<AuthorDTO> Authors()
		{
			GenericRepository<Authors> repository = new GenericRepository<Authors>(_context);
			List<AuthorDTO> authors = repository.Get().Select(c => (AuthorDTO)c).ToList();

			return authors;
		}

		public List<DepartmentsGroupedDTO> Departments()
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<DepartmentsGroupedDTO> departments = procedure.DepartmentList().Select(c => (DepartmentsGroupedDTO)c).ToList();

			return departments;
		}

		public List<DepartmentsGroupedDTO> Departments(string symbols)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<DepartmentsGroupedDTO> departments = procedure.DepartmentList(symbols).Select(c => (DepartmentsGroupedDTO)c).ToList();

			return departments;
		}

		public LibraryDTO LibraryInfo(int libId)
		{
			GenericRepository<Libraries> generic = new GenericRepository<Libraries>(_context);

			LibraryDTO library = (LibraryDTO)generic.FindById(libId);

			return library;
		}


		public List<ShopDTO> Shops()
		{
			GenericRepository<Shops> generic = new GenericRepository<Shops>(_context);
			List<ShopDTO> shops = generic.Get().Select(c => (ShopDTO)c).ToList();

			return shops;
		}

		public List<OrderDTO> Orders(int supplyId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<OrderDTO> orders = procedure.OrderList().Where(c => c.SupplyId == supplyId).Select(c => (OrderDTO)c).ToList();

			return orders;
		}

		public ProviderDataDTO ProviderInfo(int providerId)
		{
			GenericRepository<Providers> generic = new GenericRepository<Providers>(_context);

			ProviderDataDTO provider = (ProviderDataDTO)generic.Get(c => c.Id == providerId).FirstOrDefault();

			return provider;
		}

		public List<SupplyDetailedDTO> SuppliesList()
		{
			List<SupplyDetailedDTO> supplies = _context.Supplies.Join(_context.Shops, sup => sup.Shop, sh => sh.Id, (sup, sh) => new SupplyDetailedDTO
			{
				Shop = new ShopDTO
				{
					Id = sh.Id,
					Name = sh.Name,
					Address = sh.Address,
					Phone = sh.Phone
				},
				Supply = new SupplyDTO
				{
					Id = sup.Id,
					ShopId = sup.Shop,
					Summ = sup.Summ,
					Date = sup.Date
				}
			}).ToList();

			return supplies;
		}

		public ResultModel AddOrder(OrderDTO order)
		{
			ResultModel result = new ResultModel();

			BookDTO book = new BookDTO
			{
				AuthorId = order.Author.Id,
				Title = order.Book.Title
			};

			int bookId = AddBook(book);

			if (bookId > 0)
			{

				using (var transaction = _context.Database.BeginTransaction())
				{
					try
					{
						OrderLists item = new OrderLists
						{
							Book = bookId,
							Cost = order.Cost,
							Supply = order.Supply.Id
						};

						OrderLists item1 = (OrderLists)order;
						item.Book = bookId;

						_context.OrderLists.Add(item);

						Supplies supply = _context.Supplies.FirstOrDefault(c => c.Id == order.Supply.Id);

						double oldSumm = supply.Summ.HasValue ? supply.Summ.Value : 0;
						supply.Summ = oldSumm + order.Cost;
						_context.Entry(supply).State = EntityState.Modified;

						_context.SaveChanges();
						transaction.Commit();
					}
					catch (Exception ex)
					{
						result.Code = OperationStatusEnum.UnexpectedError;
						result.Message = ex.StackTrace;
						transaction.Rollback();
					}
				}
			}
			else
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = "Ошибка при добавлении книги";
			}

			return result;
		}

		private int AddBook (BookDTO book)
		{
			try
			{
				Books item = (Books)book;

				_context.Books.Add(item);
				_context.SaveChanges();

				var inserted = _context.Books.Max(c => c.Id);

				if (inserted > 0)
				{
					return inserted;
				}

				return 0;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}

		public int AddAuthor(AuthorDTO author)
		{
			try
			{
				Authors item = (Authors)author;
				_context.Authors.Add(item);
				_context.SaveChanges();

				var inserted = _context.Authors.Max(c => c.Id);

				if (inserted > 0)
				{
					return inserted;
				}

				return 0;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}
