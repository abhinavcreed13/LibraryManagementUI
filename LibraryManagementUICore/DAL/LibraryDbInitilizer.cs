﻿using LibraryManagementUICore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementUICore.DAL
{
    public static class LibraryDbInitilizer
    {
        public static void Seed(LibraryDbContext context)
        {
            if (context.Books.Count() == 0)
            {
                List<Book> books = new List<Book>
            {
                new Book { Title = "title1", SerialNumber="ABCD1", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title2", SerialNumber="ABCD2", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title3", SerialNumber="ABCD3", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title4", SerialNumber="ABCD4", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title5", SerialNumber="ABCD5", Author="Auth1", Publisher="Pub1"}
            };

                foreach (Book book in books)
                {
                    context.Books.Add(book);
                }

                //save in database
                context.SaveChanges();
            }

            if (context.Customers.Count() == 0)
            {
                List<Customer> customers = new List<Customer>()
            {
                new Customer { Name = "Customer1", Address="Add1", Contact="Conct1"},
                new Customer { Name = "Customer2", Address="Add2", Contact="Conct2"},
                new Customer { Name = "Customer3", Address="Add3", Contact="Conct3"},
                new Customer { Name = "Customer4", Address="Add4", Contact="Conct4"},
                new Customer { Name = "Customer5", Address="Add5", Contact="Conct5"}
            };

                customers.ForEach(customer => context.Customers.Add(customer));

                //save in database
                context.SaveChanges();
            }
        }
    }
}
