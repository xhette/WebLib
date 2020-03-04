using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer.Procedures;

namespace WebLib.BusinessLayer.DTO.Composite
{
	public class IssueDetailedDTO
	{
		public IssueDTO Issue { get; set; }

		public BookDetailedDTO Book { get; set; }

		public ReaderShortDataDTO Reader { get; set; }

		public static explicit operator IssueDetailedDTO (IssueDetailed dbIssue)
		{
			if (dbIssue == null) return null;
			else return new IssueDetailedDTO
			{
				Issue = new IssueDTO
				{
					Id = dbIssue.IssueId,
					IssueDate = dbIssue.IssueDate,
					ReturnDate = dbIssue.ReturnDate
				},

				Book = new BookDetailedDTO
				{
					AuthorId = dbIssue.AuthorId,
					AuthorSurname = dbIssue.AuthorSurname,
					AuthorName = dbIssue.AuthorName,
					AuthorPatronymic = dbIssue.AuthorPatronymic,

					BookId = dbIssue.BookId,
					Title = dbIssue.Title,
					DepartmentId = dbIssue.DepartmentId,
					DepartmentName = dbIssue.DepartmentName,
					LibraryId = dbIssue.LibraryId,
					LibraryName = dbIssue.LibraryName
				},

				Reader = new ReaderShortDataDTO
				{
					Id = dbIssue.ReaderId,
					Surname = dbIssue.ReaderSurname,
					Name = dbIssue.ReaderName,
					Patronymic = dbIssue.ReaderPatronymic
				}
			};
		}
	}
}
