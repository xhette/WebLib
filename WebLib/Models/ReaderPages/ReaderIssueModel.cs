using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLib.BusinessLayer.DTO.Composite;
using WebLib.Enums;

namespace WebLib.Models.ReaderPages
{
	public class ReaderIssueModel
	{

		public int Id { get; set; }

		public DateTime? IssueDate { get; set; }

		public DateTime? ReturnDate { get; set; }

		public BookViewModel Book { get; set; }

		public IssueStatusEnum Status { get; set; }

		public static explicit operator ReaderIssueModel (IssueDetailedDTO dto)
		{
			if (dto == null) return null;
			else
			{
				ReaderIssueModel reader = new ReaderIssueModel
				{
					Id = dto.Issue.Id,
					IssueDate = dto.Issue.IssueDate,
					ReturnDate = dto.Issue.ReturnDate,

					Book = (BookViewModel)dto.Book
				};


				if (reader.IssueDate != null && reader.ReturnDate == null)
				{
					DateTime deadLine = reader.IssueDate.Value.AddDays(14);
					if (DateTime.Compare(DateTime.Today, deadLine) < 0)
						reader.Status = IssueStatusEnum.Spoiled;
					else reader.Status = IssueStatusEnum.Processed;
				}
				else if (reader.IssueDate != null && reader.ReturnDate != null)
					reader.Status = IssueStatusEnum.Returned;

				return reader;
			}
		}
	}
}