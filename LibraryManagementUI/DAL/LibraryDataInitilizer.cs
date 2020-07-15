using LibraryManagementUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagementUI.DAL
{
    public class LibraryDataInitilizer: DropCreateDatabaseIfModelChanges<LibraryDBContext>
    {
        protected override void Seed(LibraryDBContext context)
        {
            List<Book> books = new List<Book>
            {
                new Book { Title = "title1", SerialNumber="ABCD1", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title2", SerialNumber="ABCD2", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title3", SerialNumber="ABCD3", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title4", SerialNumber="ABCD4", Author="Auth1", Publisher="Pub1"},
                new Book { Title = "title5", SerialNumber="ABCD5", Author="Auth1", Publisher="Pub1"}
            };

            foreach(Book book in books)
            {
                context.Books.Add(book);
            }

            //save in database
            context.SaveChanges();

            List<Customer> customers = new List<Customer>()
            {
                new Customer { Name = "Customer1", Address="Add1", Contact="Conct1"},
                new Customer { Name = "Customer2", Address="Add2", Contact="Conct2"},
                new Customer { Name = "Customer3", Address="Add3", Contact="Conct3"},
                new Customer { Name = "Customer4", Address="Add4", Contact="Conct4"},
                new Customer { Name = "Customer5", Address="Add5", Contact="Conct5"}
            };

            //foreach(Customer customer in customers)
            //{
            //    context.Customers.Add(customer);
            //}

            // delegate: function pointer
            // Action delegate: void functions
            void AddCustomer(Customer customer)
            {
                context.Customers.Add(customer);
            }

            Action<Customer> addCustomerDelegate = new Action<Customer>(AddCustomer);

            customers.ForEach(customer => context.Customers.Add(customer));

            //Func delegate: function will return something
            int id = 56;
            bool IsCustomerIdValid(Customer customer)
            {
                return customer.CustomerId == id;
            }

            //Predicate delegate
            Func<Customer, bool> getCustomerIdDelegate = new Func<Customer, bool>(IsCustomerIdValid);

            // Predicate delegate: Func<Customer,bool>

            
            customers.Find(customer => customer.CustomerId == id);

            //save in database
            context.SaveChanges();
        }
    }
}