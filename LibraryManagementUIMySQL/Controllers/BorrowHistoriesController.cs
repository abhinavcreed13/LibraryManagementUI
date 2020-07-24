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
    public class BorrowHistoriesController : Controller
    {
        public static DataTable BorrowHistories;
        // GET: BorrowHistories
        public ActionResult Index()
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
            List<BorrowHistory> borrowhistories = new List<BorrowHistory>();
            foreach(DataRow dr in BorrowHistories.Rows)
            {
                borrowhistories.Add(new BorrowHistory
                {
                    BookId = dr.Field<int>("BookId"),
                    Title = dr.Field<string>("Title"),
                    Name = dr.Field<string>("Name"),
                    CustomerId = dr.Field<int>("CustomerId"),
                    BorrowDate = dr.Field<DateTime>("BorrowDate"),
                    ReturnDate = dr.Field<DateTime?> ("ReturnDate")
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: BorrowHistories/Create
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

        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BorrowHistories/Edit/5
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
