using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebLib.BusinessLayer.BusinessModels;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods
{
	public class ReaderPage
	{
		LibContext _context;

		public ReaderPage (LibContext context)
		{
			_context = context;
		}

		public ReaderDataDTO ReaderData (int userId)
		{
			GenericRepository<Readers> repository = new GenericRepository<Readers>(_context);
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

		public ResultModel AddAbonentClaim (int readerId, int libId)
		{
			ResultModel result = new ResultModel();
			try
			{
				GenericRepository<AbonentLists> generic = new GenericRepository<AbonentLists>(_context);

				AbonentLists abonent = new AbonentLists
				{
					AbonentStatus = 1,
					Library = libId,
					Reader = readerId,
					ReaderCard = ReaderCardGenerator.Generate(readerId, libId)
				};

				generic.Create(abonent);

				result.Message = "Ваша заявка принята к рассмотрению.";
			}
			catch (Exception exc)
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = exc.Message;
			}

			return result;
		}

		public ResultModel UpdateReader(ReaderDataDTO reader)
		{
			ResultModel result = new ResultModel();

			try
			{
				GenericRepository<Readers> generic = new GenericRepository<Readers>(_context);

				Readers dbReader = (Readers)reader;
				generic.Update(dbReader);

				result.Message = "Данные успешно оьновлены.";
			}
			catch (Exception exc)
			{
				result.Code = OperationStatusEnum.UnexpectedError;
				result.Message = exc.Message;
			}

			return result;
		}
	}
}
