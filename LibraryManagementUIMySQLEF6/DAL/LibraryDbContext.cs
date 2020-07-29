using LibraryManagementUIMySQLEF6.Models;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagementUIMySQLEF6.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LibraryDbContext: DbContext
    { 
        public LibraryDbContext(): base("mySqlConnString")
        {

        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<BorrowHistory> BorrowHistories { get; set; }
    }
}