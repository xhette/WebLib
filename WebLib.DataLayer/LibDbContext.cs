namespace WebLib.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibDbContext : DbContext
    {
        public LibDbContext()
            : base("name=LibDbConnection")
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<Librarian> Librarian { get; set; }
        public virtual DbSet<Library> Library { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Reader> Reader { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Supply> Supply { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<AbonentList> AbonentList { get; set; }
        public virtual DbSet<OrderList> OrderList { get; set; }

        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }

        public virtual DbSet<C_temp_Author> C_temp_Author { get; set; }
        public virtual DbSet<C_temp_Book> C_temp_Book { get; set; }
        public virtual DbSet<C_temp_City> C_temp_City { get; set; }
        public virtual DbSet<C_temp_Department> C_temp_Department { get; set; }
        public virtual DbSet<C_temp_Issue> C_temp_Issue { get; set; }
        public virtual DbSet<C_temp_Librarian> C_temp_Librarian { get; set; }
        public virtual DbSet<C_temp_Library> C_temp_Library { get; set; }
        public virtual DbSet<C_temp_Provider> C_temp_Provider { get; set; }
        public virtual DbSet<C_temp_Reader> C_temp_Reader { get; set; }
        public virtual DbSet<C_temp_Shop> C_temp_Shop { get; set; }
        public virtual DbSet<C_temp_Supply> C_temp_Supply { get; set; }
        public virtual DbSet<C_temp_Users> C_temp_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.C_temp_Book)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Book)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.C_temp_Issue)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.BookId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Issue)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.BookId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.OrderList)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.BookId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.C_temp_Library)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.CityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Library)
                .WithRequired(e => e._City)
                .HasForeignKey(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Book)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.C_temp_Book)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Department)
                .WithRequired(e => e._Library)
                .HasForeignKey(e => e.Library)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Librarian)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.C_temp_Department)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.C_temp_Librarian)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.AbonentList)
                .WithRequired(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.PassSeria)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.PassNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .HasMany(e => e.Issue)
                .WithRequired(e => e.Reader)
                .HasForeignKey(e => e.ReaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reader>()
                .HasMany(e => e.C_temp_Issue)
                .WithRequired(e => e.Reader)
                .HasForeignKey(e => e.ReaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reader>()
                .HasMany(e => e.AbonentList)
                .WithRequired(e => e.Reader)
                .HasForeignKey(e => e.ReaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Shop>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Shop>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.C_temp_Supply)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Supply)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Summ)
                .HasPrecision(10, 10);

            modelBuilder.Entity<Supply>()
                .HasMany(e => e.OrderList)
                .WithRequired(e => e.Supply)
                .HasForeignKey(e => e.SupplyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Librarian)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Provider)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reader)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.C_temp_Librarian)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.C_temp_Provider)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.C_temp_Reader)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<AbonentList>()
                .Property(e => e.ReaderCard)
                .IsUnicode(false);

            modelBuilder.Entity<OrderList>()
                .Property(e => e.Cost)
                .HasPrecision(10, 10);

            modelBuilder.Entity<webpages_Roles> ()
                .HasMany (e => e.Users)
                .WithMany (e => e.webpages_Roles)
                .Map (m => m.ToTable ("webpages_UsersInRoles").MapLeftKey ("RoleId").MapRightKey ("UserId"));


            modelBuilder.Entity<C_temp_Author> ()
                .Property (e => e.Surname)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Author> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Author> ()
                .Property (e => e.Patronymic)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Author> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Author> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Book> ()
                .Property (e => e.Title)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Book> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Book> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_City> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_City> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_City> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Department> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Department> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Department> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Issue> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Issue> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.Surname)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.Patronymic)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.Address)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.Phone)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Librarian> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Library> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Library> ()
                .Property (e => e.Address)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Library> ()
                .Property (e => e.Phone)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Library> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Library> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Provider> ()
                .Property (e => e.Surname)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Provider> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Provider> ()
                .Property (e => e.Patronymic)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Provider> ()
                .Property (e => e.Address)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Provider> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Provider> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.Surname)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.Patronymic)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.PassSeria)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.PassNumber)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.Address)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.Phone)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Reader> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Shop> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Shop> ()
                .Property (e => e.Address)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Shop> ()
                .Property (e => e.Phone)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Shop> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Shop> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Supply> ()
                .Property (e => e.Summ)
                .HasPrecision (10, 10);

            modelBuilder.Entity<C_temp_Supply> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Supply> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

            modelBuilder.Entity<C_temp_Users> ()
                .Property (e => e.Name)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Users> ()
                .Property (e => e.Operation)
                .IsUnicode (false);

            modelBuilder.Entity<C_temp_Users> ()
                .Property (e => e.DateTime)
                .IsFixedLength ();

        }
    }
}
