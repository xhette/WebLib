using System.Collections.Generic;
using System.Linq;
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

		public ProviderPage (LibContext context)
		{
			_context = context;
		}

		public List<LibraryDTO> LibraryList ()
		{
			List<LibraryDTO> result;

			StoredProcedure procedure = new StoredProcedure(_context);

			result = procedure.LibraryDetailedList().Select(c => (LibraryDTO)c).ToList();
			return result;
		}

		public List<CityDTO> CityList ()
		{
			GenericRepository<Cities> repository = new GenericRepository<Cities>(_context);
			List<CityDTO> cities = repository.Get().Select(c => (CityDTO)c).ToList();

			return cities;
		}

		public List<BookDetailedDTO> Books ()
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<BookDetailedDTO> books = procedure.BookList().Select(c => (BookDetailedDTO)c).ToList();

			return books;
		}

		public List<AuthorDTO> Authors ()
		{
			GenericRepository<Authors> repository = new GenericRepository<Authors>(_context);
			List<AuthorDTO> authors = repository.Get().Select(c => (AuthorDTO)c).ToList();

			return authors;
		}

		public List<DepartmentsGroupedDTO> Departments ()
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<DepartmentsGroupedDTO> departments = procedure.DepartmentList().Select(c => (DepartmentsGroupedDTO)c).ToList();

			return departments;
		}

		public List<DepartmentsGroupedDTO> Departments (string symbols)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<DepartmentsGroupedDTO> departments = procedure.DepartmentList(symbols).Select(c => (DepartmentsGroupedDTO)c).ToList();

			return departments;
		}

		public LibraryDTO LibraryInfo (int libId)
		{
			GenericRepository<Libraries> generic = new GenericRepository<Libraries>(_context);

			LibraryDTO library = (LibraryDTO)generic.FindById(libId);

			return library;
		}


		public List<ShopDTO> Shops ()
		{
			GenericRepository<Shops> generic = new GenericRepository<Shops>(_context);
			List<ShopDTO> shops = generic.Get().Select(c => (ShopDTO)c).ToList();

			return shops;
		}

		public List<OrderDTO> Orders ()
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<OrderDTO> orders = procedure.OrderList().Select(c => (OrderDTO)c).ToList();

			return orders;
		}
	}
}
