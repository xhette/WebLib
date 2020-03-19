using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO;

namespace WebLib.Models
{
	public class IssueModel
	{
		public int Id { get; set; }

		public DateTime? IssueDate { get; set; }

		public DateTime? ReturnDate { get; set; }

		public int BookId { get; set; }

		public int ReaderId { get; set; }

		public int UserId { get; set; }

		public static explicit operator IssueDTO(IssueModel dbIssue)
		{
			if (dbIssue == null) return null;
			else return new IssueDTO
			{
				Id = dbIssue.Id,
				IssueDate = dbIssue.IssueDate,
				ReturnDate = dbIssue.ReturnDate,
				BookId = dbIssue.BookId,
				ReaderId = dbIssue.ReaderId
			};
		}

		public static explicit operator IssueModel(IssueDTO dbIssue)
		{
			if (dbIssue == null) return null;
			else return new IssueModel
			{
				Id = dbIssue.Id,
				IssueDate = dbIssue.IssueDate,
				ReturnDate = dbIssue.ReturnDate,
				BookId = dbIssue.BookId,
				ReaderId = dbIssue.ReaderId
			};
		}
	}
}