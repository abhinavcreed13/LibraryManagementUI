using LibraryManagementUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LibraryManagementUI.DAL
{
    public class LibraryDBContext: DbContext
    {
        public LibraryDBContext(): base("dbConnKey")
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        //function overridding
        //virtual + override
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //avoid plural names of tables
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}