using System.Collections.Generic;
using System.Linq;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;

namespace WebLib.BusinessLayer.GeneralMethods
{
	public class ReaderPage
	{
		LibDbContext _context;

		public ReaderPage (LibDbContext context)
		{
			_context = context;
		}

		public ReaderDataDTO ReaderData (int userId)
		{
			GenericRepository<Reader> repository = new GenericRepository<Reader>(_context);
			return (ReaderDataDTO)repository.Get(c => c.UserId == userId).FirstOrDefault();
		}

		public List<IssueDetailedDTO> ReaderIssuesList (int readerId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			return procedure.IssueList().Where(c => c.ReaderId == readerId).Select(c => (IssueDetailedDTO)c).ToList();
		}

		public List<AbonentInLibraryDTO> AbonentList (int readerId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			return procedure.Abonents().Where(c => c.ReaderId == readerId && c.AbonentStatus == 3).Select(c => (AbonentInLibraryDTO)c).ToList();
		}

		public List<AbonentInLibraryDTO> LibraryInfoAbonent (int readerId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			return procedure.LibrariesWithAbonents().Select(c => (AbonentInLibraryDTO)c).ToList();
		}

		public List<LibraryDTO> LibraryList ()
		{
			List<LibraryDTO> result;

			StoredProcedure procedure = new StoredProcedure(_context);

			result = procedure.LibraryDetailedList().Select(c => (LibraryDTO)c).ToList();
			return result;
		}

		public List<LibraryDTO> LibraryList (int cityId)
		{
			List<LibraryDTO> result;

			StoredProcedure procedure = new StoredProcedure(_context);

			result = procedure.LibraryDetailedList().Where (c => c.CityId == cityId)
				.Select(c => (LibraryDTO)c).ToList();

			return result;

		}

		public List<CityDTO> CityList ()
		{
			GenericRepository<City> repository = new GenericRepository<City>(_context);
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
			GenericRepository<Author> repository = new GenericRepository<Author>(_context);
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
			GenericRepository<Library> generic = new GenericRepository<Library>(_context);

			LibraryDTO library = (LibraryDTO)generic.FindById(libId);

			return library;
		}

		public bool AddAbonentClaim (int readerId, int libId)
		{
			try
			{
				GenericRepository<AbonentList> generic = new GenericRepository<AbonentList>(_context);

				AbonentList abonent = new AbonentList
				{
					AbonentStatus = 1,
					Library = libId,
					Reader = readerId,
					ReaderCard = ReaderCardGenerator.Generate(readerId, libId)
				};

				generic.Create(abonent);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateReader(ReaderDataDTO reader)
		{
			try
			{
				GenericRepository<Reader> generic = new GenericRepository<Reader>(_context);

				Reader dbReader = (Reader)reader;
				generic.Update(dbReader);

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
