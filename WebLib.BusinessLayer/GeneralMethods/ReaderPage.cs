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
			return procedure.Abonents().Where(c => c.ReaderId == readerId).Select(c => (AbonentInLibraryDTO)c).ToList();
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
	}
}
