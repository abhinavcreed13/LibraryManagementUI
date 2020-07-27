using DataAccessLayer;
using LibraryManagementUIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementUIMySQL.Controllers
{
    public class CustomersController : Controller
    {
        public static DataTable Customers;

        public static void InitilizeCustomer()
        {
            try
            {
                MySQLDALManager manager = new MySQLDALManager("mySQLDbConnKey");
                Customers = manager.ExecuteStoredProcedure("GetAllCustomers");
            }
            catch (Exception ex)
            {
                Customers = new DataTable();
            }
        }

        // GET: Customers
        public ActionResult Index()
        {
            InitilizeCustomer();
            List<Customer> customers = new List<Customer>();
            foreach (DataRow dr in Customers.Rows)
            {
                customers.Add(new Customer
                {
                    CustomerId = dr.Field<int>("CustomerId"),
                    Name = dr.Field<string>("Name"),
                    Contact = dr.Field<string>("Contact"),
                    Address = dr.Field<string>("Address")
                });
            }

            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            DataRow foundRow = Customers.AsEnumerable()
                                    .Where(row => row.Field<int>("CustomerId") == id)
                                        .FirstOrDefault();
            if (foundRow == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            var foundCustomer = new Customer
            {
                CustomerId = foundRow.Field<int>("CustomerId"),
                Name = foundRow.Field<string>("Name"),
                Contact = foundRow.Field<string>("Contact"),
                Address = foundRow.Field<string>("Address")
            };
            return View(foundCustomer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
