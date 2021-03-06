﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.GeneralMethods
{
	public class LibrarianPage
	{
		LibContext _context;

		public LibrarianPage (LibContext context)
		{
			_context = context;
		}

		public LibrarianDataDTO LibrarianData (int librarianId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			var lib = (LibrarianDataDTO)procedure.LibrarianList().FirstOrDefault(c => c.LibrarianId == librarianId);

			return lib;
		}


		public List<BookDetailedDTO> Books (int libId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			List<BookDetailedDTO> books = procedure.BookList().Where(c => c.LibraryId == libId).Select(c => (BookDetailedDTO)c).ToList();

			return books;
		}

		public List<AuthorDTO> Authors ()
		{
			GenericRepository<Authors> repository = new GenericRepository<Authors>(_context);
			List<AuthorDTO> authors = repository.Get().Select(c => (AuthorDTO)c).ToList();

			return authors;
		}

		public DepartmentsGroupedDTO Departments (int libId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			DepartmentsGroupedDTO departments = (DepartmentsGroupedDTO)procedure.DepartmentList().FirstOrDefault(c => c.Library.Id == libId);

			return departments;
		}

		public DepartmentsGroupedDTO Departments (string symbols, int libId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			DepartmentsGroupedDTO departments = (DepartmentsGroupedDTO)procedure.DepartmentList(symbols).FirstOrDefault(c => c.Library.Id == libId);

			return departments;
		}

		public List<AbonentInLibraryDTO> AbonentList (int libId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			return procedure.Abonents().Where(c => c.LibraryId == libId).Select(c => (AbonentInLibraryDTO)c).ToList();
		}

		public List<AbonentInLibraryDTO> AbonentListSpoiled(int libId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			return procedure.AbonentsSpoiled().Where(c => c.LibraryId == libId).Select(c => (AbonentInLibraryDTO)c).ToList();
		}

		public List<IssueDetailedDTO> IssuesList (int libId)
		{
			StoredProcedure procedure = new StoredProcedure(_context);
			return procedure.IssueList().Where(c => c.LibraryId == libId).Select(c => (IssueDetailedDTO)c).ToList();
		}

		public bool DeleteAbonent (int libId, int readerId)
		{
			try
			{
				GenericRepository<AbonentLists> generic = new GenericRepository<AbonentLists>(_context);
				var abonent = generic.Get(c => c.Reader == readerId && c.Library == libId).FirstOrDefault();
				
				if (abonent != null)
				{
					generic.Remove(abonent);

					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public bool ChangeStatusAbonent (int libId, int readerId, int status)
		{
			try
			{
				GenericRepository<AbonentLists> generic = new GenericRepository<AbonentLists>(_context);
				var abonent = generic.Get(c => c.Reader == readerId && c.Library == libId).FirstOrDefault();

				if (abonent != null)
				{
					abonent.AbonentStatus = status;
					generic.Update(abonent);

					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public bool AddIssue (int bookId, int readerId)
		{
			try
			{
				GenericRepository<Issues> generic = new GenericRepository<Issues>(_context);
					Issues newIssue = new Issues
					{
						Book = bookId,
						Reader = readerId,
						IssueDate = DateTime.Today,
						ReturnDate = null
					};


					generic.Create(newIssue);

					return true;

			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool UpdateIssue (IssueDTO issue)
		{
			try
			{
				GenericRepository<Issues> generic = new GenericRepository<Issues>(_context);

				Issues db = (Issues)issue;

				generic.Update(db);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteIssue (int issueId)
		{
			try
			{
				GenericRepository<Issues> generic = new GenericRepository<Issues>(_context);

				Issues db = generic.FindById(issueId);

				if (db != null)
				{
					generic.Remove(db);
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public ReaderDataDTO ReaderInfo (int rederId)
		{
			GenericRepository<Readers> repository = new GenericRepository<Readers>(_context);
			return (ReaderDataDTO)repository.Get(c => c.Id == rederId).FirstOrDefault();
		}

		public bool GiveOut(int bookId)
		{
			try
			{
				GenericRepository<Issues> generic = new GenericRepository<Issues>(_context);

				Issues db = generic.Get(c => c.Book == bookId && c.ReturnDate == null).FirstOrDefault();

				if(db == null)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
		}
	}
}
