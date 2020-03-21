namespace WebLib.DataLayer
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
    using WebLib.DataLayer.Base;

    public partial class LibContext : DbContext
	{
		public LibContext ()
			: base("name=LibDbConnection")
		{
		}
		public virtual DbSet<Authors> Authors { get; set; }
		public virtual DbSet<Books> Books { get; set; }
		public virtual DbSet<Cities> Cities { get; set; }
		public virtual DbSet<Departments> Departments { get; set; }
		public virtual DbSet<Issues> Issues { get; set; }
		public virtual DbSet<Librarians> Librarians { get; set; }
		public virtual DbSet<Libraries> Libraries { get; set; }
		public virtual DbSet<Providers> Providers { get; set; }
		public virtual DbSet<Readers> Readers { get; set; }
		public virtual DbSet<Shops> Shops { get; set; }
		public virtual DbSet<Supplies> Supplies { get; set; }
		public virtual DbSet<AbonentLists> AbonentLists { get; set; }
		public virtual DbSet<OrderLists> OrderLists { get; set; }

		public virtual DbSet<Users> Users { get; set; }
		public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
		public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
		public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
		public virtual DbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }

		public virtual DbSet<AuthorsHistory> AuthorsHistory { get; set; }
		public virtual DbSet<BooksHistory> BooksHistory { get; set; }
		public virtual DbSet<CitiesHistory> CitiesHistory { get; set; }
		public virtual DbSet<DepartmentsHistory> DepartmentsHistory { get; set; }
		public virtual DbSet<IssuesHistory> IssuesHistory { get; set; }
		public virtual DbSet<LibrariansHistory> LibrariansHistory { get; set; }
		public virtual DbSet<LibrariesHistory> LibrariesHistory { get; set; }
		public virtual DbSet<ProvidersHistory> ProvidersHistory { get; set; }
		public virtual DbSet<ReadersHistory> ReadersHistory { get; set; }
		public virtual DbSet<ShopsHistory> ShopsHistory { get; set; }
		public virtual DbSet<SuppliesHistory> SuppliesHistory { get; set; }

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Authors>()
				.Property(e => e.Surname)
				.IsUnicode(false);

			modelBuilder.Entity<Authors>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Authors>()
				.Property(e => e.Patronymic)
				.IsUnicode(false);

			modelBuilder.Entity<Authors>()
				.HasMany(e => e.Books)
				.WithRequired(e => e.Authors)
				.HasForeignKey(e => e.Author);

			modelBuilder.Entity<Books>()
				.Property(e => e.Title)
				.IsUnicode(false);

			modelBuilder.Entity<Books>()
				.HasMany(e => e.Issues)
				.WithRequired(e => e.Books)
				.HasForeignKey(e => e.Book);

			modelBuilder.Entity<Books>()
				.HasMany(e => e.OrderLists)
				.WithRequired(e => e.Books)
				.HasForeignKey(e => e.Book);

			modelBuilder.Entity<Cities>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Cities>()
				.HasMany(e => e.Libraries)
				.WithRequired(e => e.Cities)
				.HasForeignKey(e => e.City);

			modelBuilder.Entity<Departments>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Departments>()
				.HasMany(e => e.Books)
				.WithOptional(e => e.Departments)
				.HasForeignKey(e => e.Department)
				.WillCascadeOnDelete();

			modelBuilder.Entity<Librarians>()
				.Property(e => e.Surname)
				.IsUnicode(false);

			modelBuilder.Entity<Librarians>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Librarians>()
				.Property(e => e.Patronymic)
				.IsUnicode(false);

			modelBuilder.Entity<Librarians>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Librarians>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Libraries>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Libraries>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Libraries>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Libraries>()
				.HasMany(e => e.Departments)
				.WithRequired(e => e.Libraries)
				.HasForeignKey(e => e.Library);

			modelBuilder.Entity<Libraries>()
				.HasMany(e => e.Librarians)
				.WithRequired(e => e.Libraries)
				.HasForeignKey(e => e.Library);

			modelBuilder.Entity<Libraries>()
				.HasMany(e => e.AbonentLists)
				.WithRequired(e => e.Libraries)
				.HasForeignKey(e => e.Library);

			modelBuilder.Entity<Providers>()
				.Property(e => e.Surname)
				.IsUnicode(false);

			modelBuilder.Entity<Providers>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Providers>()
				.Property(e => e.Patronymic)
				.IsUnicode(false);

			modelBuilder.Entity<Providers>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.Surname)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.Patronymic)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.PassSeria)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.PassNumber)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Readers>()
				.HasMany(e => e.Issues)
				.WithRequired(e => e.Readers)
				.HasForeignKey(e => e.Reader);

			modelBuilder.Entity<Readers>()
				.HasMany(e => e.AbonentLists)
				.WithRequired(e => e.Readers)
				.HasForeignKey(e => e.Reader);

			modelBuilder.Entity<Shops>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Shops>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Shops>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Shops>()
				.HasMany(e => e.Supplies)
				.WithRequired(e => e.Shops)
				.HasForeignKey(e => e.Shop);

			modelBuilder.Entity<Supplies>()
				.Property(e => e.Summ)
				.HasPrecision(10, 10);

			modelBuilder.Entity<Supplies>()
				.HasMany(e => e.OrderLists)
				.WithRequired(e => e.Supplies)
				.HasForeignKey(e => e.Supply);

			modelBuilder.Entity<Users>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Librarians)
				.WithOptional(e => e.Users)
				.HasForeignKey(e => e.UserId);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Providers)
				.WithOptional(e => e.Users)
				.HasForeignKey(e => e.UserId);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Readers)
				.WithRequired(e => e.Users)
				.HasForeignKey(e => e.UserId);

			modelBuilder.Entity<AbonentLists>()
				.Property(e => e.ReaderCard)
				.IsUnicode(false);

			modelBuilder.Entity<OrderLists>()
				.Property(e => e.Cost)
				.HasPrecision(10, 10);

			modelBuilder.Entity<webpages_Roles>()
				.HasMany(e => e.webpages_UsersInRoles)
				.WithRequired(e => e.webpages_Roles)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.HistorySurname)
				.IsUnicode(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.HistoryPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.CurrentSurname)
				.IsUnicode(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.CurrentPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<AuthorsHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<BooksHistory>()
				.Property(e => e.HistoryTitle)
				.IsUnicode(false);

			modelBuilder.Entity<BooksHistory>()
				.Property(e => e.CurrentTitle)
				.IsUnicode(false);

			modelBuilder.Entity<BooksHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<CitiesHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<CitiesHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<CitiesHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<DepartmentsHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<DepartmentsHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<DepartmentsHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<IssuesHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.HistorySurname)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.HistoryPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.HistoryAddress)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.HistoryPhone)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.CurrentSurname)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.CurrentPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.CurrentAddress)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.CurrentPhone)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariansHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.HistoryAddress)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.HistoryPhone)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.CurrentAddress)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.CurrentPhone)
				.IsUnicode(false);

			modelBuilder.Entity<LibrariesHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.HistorySurname)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.HistoryPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.HistoryAddress)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.CurrentSurname)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.CurrentPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.CurrentAddress)
				.IsUnicode(false);

			modelBuilder.Entity<ProvidersHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistorySurname)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistoryPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistoryPassSeria)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistoryPassNumber)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistoryAddress)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.HistoryPhone)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentSurname)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentPatronymic)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentPassSeria)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentPassNumber)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentAddress)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.CurrentPhone)
				.IsUnicode(false);

			modelBuilder.Entity<ReadersHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.HistoryName)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.HistoryAddress)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.HistoryPhone)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.CurrentName)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.CurrentAddress)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.CurrentPhone)
				.IsUnicode(false);

			modelBuilder.Entity<ShopsHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);

			modelBuilder.Entity<SuppliesHistory>()
				.Property(e => e.HistorySumm)
				.HasPrecision(10, 10);

			modelBuilder.Entity<SuppliesHistory>()
				.Property(e => e.CurrentSumm)
				.HasPrecision(10, 10);

			modelBuilder.Entity<SuppliesHistory>()
				.Property(e => e.Operation)
				.IsUnicode(false);
		}
	}
}
