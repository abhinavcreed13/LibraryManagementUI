using DataAccessLayer;
using LibraryManagementUIMySQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementUIMySQL.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        public static DataTable BorrowHistories;

        public static void InitilizeBorrowHistories()
        {
            try
            {
                MySQLDALManager manager = new MySQLDALManager("mySQLDbConnKey");
                BorrowHistories = manager.ExecuteStoredProcedure("GetAllBorrowHistories");
            }
            catch (Exception ex)
            {
                BorrowHistories = new DataTable();
            }
        }

        // GET: BorrowHistories
        public ActionResult Index()
        {
            InitilizeBorrowHistories();
            List<BorrowHistory> borrowhistories = new List<BorrowHistory>();
            foreach (DataRow dr in BorrowHistories.Rows)
            {
                borrowhistories.Add(new BorrowHistory
                {
                    BookId = dr.Field<int>("BookId"),
                    Title = dr.Field<string>("Title"),
                    Name = dr.Field<string>("Name"),
                    CustomerId = dr.Field<int>("CustomerId"),
                    BorrowDate = dr.Field<DateTime>("BorrowDate"),
                    ReturnDate = dr.Field<DateTime?>("ReturnDate")
                });
            }
            return View(borrowhistories);
        }

        // GET: BorrowHistories/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BorrowHistories/Create
        public ActionResult Create(int id)
        {
            var borrowHistory = new BorrowHistory
            {
                BookId = id,
                BorrowDate = DateTime.Now
            };
            if (CustomersController.Customers == null)
            {
                CustomersController.InitilizeCustomer();
            }
            var customersData = CustomersController.Customers;
            //DataTable
            
            var customers = customersData.AsEnumerable().Select(row => new Customer
            {
                CustomerId = row.Field<int>("CustomerId"),
                Name = row.Field<string>("Name")
            }).ToList();

            ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name");
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Create
        [HttpPost]
        public ActionResult Create(BorrowHistory borrowHistory)
        {
            try
            {
                // TODO: Add insert logic here
                //  mysql proc
                MySQLDALManager manager = new MySQLDALManager("mySQLDbConnKey");
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("book_id", borrowHistory.BookId),
                    new MySqlParameter("customer_id", borrowHistory.CustomerId),
                    new MySqlParameter("borrow_date", borrowHistory.BorrowDate)
                };
                manager.ExecuteStoredProcedure("BorrowBook", parameters);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int id)
        {
            InitilizeBorrowHistories();
            var borrowHistoryRow = BorrowHistories.AsEnumerable()
                                        .Where(row => row.Field<int>("BookId") == id
                                                  && row.Field<DateTime?>("ReturnDate") == null)
                                            .FirstOrDefault();
            if(borrowHistoryRow == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            var borrowHistory = new BorrowHistory
            {
                BorrowHistoryId = borrowHistoryRow.Field<int>("BorrowHistoryId"),
                Title = borrowHistoryRow.Field<string>("Title"),
                Name = borrowHistoryRow.Field<string>("Name"),
                BorrowDate = borrowHistoryRow.Field<DateTime>("BorrowDate")
            };
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        [HttpPost]
        public ActionResult Edit(BorrowHistory borrowHistory)
        {
            try
            {
                // TODO: Add update logic here
                MySQLDALManager manager = new MySQLDALManager("mySQLDbConnKey");
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("borrow_history_id", borrowHistory.BorrowHistoryId)
                };
                manager.ExecuteStoredProcedure("ReturnBook", parameters);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: BorrowHistories/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BorrowHistories/Delete/5
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
