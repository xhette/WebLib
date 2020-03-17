using System;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages.TempTables
{
	public interface IHistory
	{
		int Undone(int current, DateTime time);

		int Redone(int current, DateTime time);
	}
}