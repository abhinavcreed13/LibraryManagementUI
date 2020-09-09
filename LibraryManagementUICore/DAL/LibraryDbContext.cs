using LibraryManagementUICore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementUICore.DAL
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions options): base(options)
        {
            // no code here
            // I will inject using Startup.cs
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // remove plural names
            // Fluent API
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<BorrowHistory>().ToTable("BorrowHistory");
        }

    }
}
