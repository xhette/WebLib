﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLib.DataLayer;
using WebLib.DataLayer.Base;

namespace WebLib.BusinessLayer.DTO
{
	public class IssueDTO
	{
		public int Id { get; set; }

		public DateTime? IssueDate { get; set; }

		public DateTime? ReturnDate { get; set; }

		public int BookId { get; set; }

		public int ReaderId { get; set; }

		public int UserId { get; set; }

		public static explicit operator IssueDTO (Issues dbIssue)
		{
			if (dbIssue == null) return null;
			else return new IssueDTO
			{
				Id = dbIssue.Id,
				IssueDate = dbIssue.IssueDate,
				ReturnDate = dbIssue.ReturnDate,
				BookId = dbIssue.Book,
				ReaderId = dbIssue.Reader
			};
		}

		public static explicit operator Issues (IssueDTO dbIssue)
		{
			if (dbIssue == null) return null;
			else return new Issues
			{
				Id = dbIssue.Id,
				IssueDate = dbIssue.IssueDate,
				ReturnDate = dbIssue.ReturnDate,
				Book = dbIssue.BookId,
				Reader = dbIssue.ReaderId
			};
		}
	}
}
