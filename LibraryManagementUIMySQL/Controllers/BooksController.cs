using DataAccessLayer;
using LibraryManagementUIMySQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementUIMySQL.Controllers
{
    public class BooksController : Controller
    {
        // saving something in server
        public static DataTable Books;

        // GET: Books
        public ActionResult Index()
        {
            //get that all books data from MySQL
            // DAL
            try
            {
                MySQLDALManager manager = new MySQLDALManager("mySQLDbConnKey");
                Books = manager.ExecuteStoredProcedure("GetAllBooks");
                TempData["Books"] = Books;
            }
            catch (Exception ex)
            {
                Books = new DataTable();
            }
            //get data from borrowhistories
            if (BorrowHistoriesController.BorrowHistories == null)
            {
                // fill that borrowhistory datatable
                BorrowHistoriesController.InitilizeBorrowHistories();
            }
            var borrowHistoryData = BorrowHistoriesController.BorrowHistories;
            List<Book> books = new List<Book>();
            foreach(DataRow dr in Books.Rows)
            {
                books.Add(new Book
                {
                    BookId = dr.Field<int>("BookId"),
                    Title = dr.Field<string>("Title"),
                    Author = dr.Field<string>("Author"),
                    Publisher = dr.Field<string>("Publisher"),
                    SerialNumber = dr.Field<string>("SerialNumber"),
                    IsAvailable = !borrowHistoryData.AsEnumerable()
                                        .Any(row => row.Field<int>("BookId") == dr.Field<int>("BookId")
                                                && row.Field<DateTime?>("ReturnDate") == null)
                });
            }
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            DataRow foundRow = null;
            //try
            //{
            //    MySQLDALManager manager = new MySQLDALManager("mySQLDbConnKey");
            //    List<MySqlParameter> parameters = new List<MySqlParameter>
            //    {
            //        new MySqlParameter("book_id",id)
            //    };
            //    foundRow = manager.ExecuteStoredProcedure("GetBook", parameters).Rows[0];
            //}
            //catch (Exception ex)
            //{
            //    // do nothing
            //}

            if(Books == null)
            {
                Books = TempData["Books"] as DataTable;
            }

            // where LINQ
            foundRow = Books.AsEnumerable()
                                .Where(row => row.Field<int>("BookId") == id)
                                    .FirstOrDefault();

            if(foundRow == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }

            var foundBook = new Book
            {
                BookId = foundRow.Field<int>("BookId"),
                Title = foundRow.Field<string>("Title"),
                Author = foundRow.Field<string>("Author"),
                Publisher = foundRow.Field<string>("Publisher"),
                SerialNumber = foundRow.Field<string>("SerialNumber")
            };

            return View(foundBook);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
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

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Books/Edit/5
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

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Books/Delete/5
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
