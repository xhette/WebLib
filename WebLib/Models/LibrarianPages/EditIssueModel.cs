using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;
using WebLib.BusinessLayer.DTO.Composite;

namespace WebLib.Models.LibrarianPages
{
	public class EditIssueModel
	{
		public int Id { get; set; }

		public List<AuthorModel> Authors { get; set; }

		public int SelectedAuthor { get; set; }

		public List<BookModel> Books { get; set; }

		[Required(ErrorMessage ="Выберите книгу")]
		public int SelectedBook { get; set; }

		public List<ReaderPages.ReaderDataModel> Readers { get; set; }

		[Required(ErrorMessage = "Выберите читателя")]
		public int SelectedReader { get; set; }

		[Required(ErrorMessage = "Введите дату выдачи")]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime? IssueDate { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime? ReturnDate { get; set; }

		public static explicit operator EditIssueModel (IssueDTO issue)
		{
			if (issue == null) return null;
			else return new EditIssueModel
			{
				Id = issue.Id,
				IssueDate = issue.IssueDate,
				ReturnDate = issue.ReturnDate,
				SelectedBook = issue.BookId,
				SelectedReader = issue.ReaderId
			};
		}

		public static explicit operator EditIssueModel(IssueDetailedDTO issue)
		{
			if (issue == null) return null;
			else return new EditIssueModel
			{
				Id = issue.Issue.Id,
				IssueDate = issue.Issue.IssueDate,
				ReturnDate = issue.Issue.ReturnDate,
				SelectedBook = issue.Issue.BookId,
				SelectedReader = issue.Issue.ReaderId,
				SelectedAuthor = issue.Book.AuthorId
			};
		}


		public static explicit operator IssueDTO (EditIssueModel model)
		{
			if (model == null) return null;
			else return new IssueDTO
			{
				BookId = model.SelectedBook,
				Id = model.Id,
				ReaderId = model.SelectedReader,
				IssueDate = model.IssueDate,
				ReturnDate = model.ReturnDate
			};
		}
	}
}