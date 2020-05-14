using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebLib.BusinessLayer.GeneralMethods.Generic;
using WebLib.DataLayer;
using WebMatrix.WebData;

namespace WebLib.Utils
{
	public static class CreateAccounts
	{
		public static SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
		public static SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;
		private static LibContext context;

		public static void Up ()
		{
			const string adminRole = "admin";
			const string readerRole = "reader";
			const string librarianRole = "librarian";
			const string providerRole = "provider";

			if (!roles.RoleExists(adminRole))
			{
				roles.CreateRole(adminRole);
			}

			if (!roles.RoleExists(readerRole))
			{
				roles.CreateRole(readerRole);
			}

			if (!roles.RoleExists(librarianRole))
			{
				roles.CreateRole(librarianRole);
			}

			if (!roles.RoleExists(providerRole))
			{
				roles.CreateRole(providerRole);
			}

			//Создание аккаунта администратора
			const string admin = "admin";
			const string adminPassword = "admin";


			if (membership.GetUser(admin, false) == null)
			{
				membership.CreateUserAndAccount(admin, adminPassword);
				if (!roles.IsUserInRole(admin, adminRole))
					roles.AddUsersToRoles(new[] { admin }, new[] { adminRole });
			}

			const string reader = "reader";
			const string readerPassword = "reader";


			if (membership.GetUser(reader, false) == null)
			{
				membership.CreateUserAndAccount(reader, readerPassword);
				if (!roles.IsUserInRole(reader, readerRole))
					roles.AddUsersToRoles(new[] { reader }, new[] { readerRole });
			}


			const string librarian = "librarian";
			const string librarianPassword = "librarian";


			if (membership.GetUser(librarian, false) == null)
			{
				membership.CreateUserAndAccount(librarian, librarianPassword);
				if (!roles.IsUserInRole(librarian, librarianRole))
					roles.AddUsersToRoles(new[] { librarian }, new[] { librarianRole });
			}

			const string provider = "provider";
			const string providerPassword = "provider";


			if (membership.GetUser(provider, false) == null)
			{
				membership.CreateUserAndAccount(provider, providerPassword);
				if (!roles.IsUserInRole(provider, providerRole))
					roles.AddUsersToRoles(new[] { provider }, new[] { providerRole });
			}



		}
		public static void DeleteUserIfExist (string userName)
		{
			if (membership.GetUser(userName, false) != null)
			{
				if (roles.GetRolesForUser(userName).Count() > 0)
				{
					roles.RemoveUsersFromRoles(new[] { userName }, roles.GetRolesForUser(userName));
				}
				membership.DeleteAccount(userName);
				membership.DeleteUser(userName, true);
			}
		}
	}
}