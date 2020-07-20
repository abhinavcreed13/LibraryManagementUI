using LibraryManagementUI.DAL;
using LibraryManagementUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementUI.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        LibraryDBContext db = new LibraryDBContext();
        
        // GET: BorrowHistories
        public ActionResult Index()
        {
            var histories = db.BorrowHistories.Include(h => h.book).Include(h => h.customer).ToList();
            return View(histories);
        }

        // GET: BorrowHistories/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BorrowHistories/Create
        public ActionResult Create(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            var borrowHistory = new BorrowHistory
            {
                BookId = book.BookId,
                BorrowDate = DateTime.Now
            };
            var customers = db.Customers.ToList();
            ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name");
            //ViewBag.customers = customers;
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Create
        [HttpPost]
        public ActionResult Create(BorrowHistory borrowHistory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.BorrowHistories.Add(borrowHistory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(borrowHistory);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            //where LINQ
            var borrowHistory = db.BorrowHistories
                                    .Include(b => b.book)
                                    .Include(b => b.customer)
                                    .Where(b => b.BookId == id && b.ReturnDate == null)
                                        .FirstOrDefault();
            if (borrowHistory == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        [HttpPost]
        public ActionResult Edit(BorrowHistory borrowHistory)
        {
            try
            {
                borrowHistory.ReturnDate = DateTime.Now;
                db.Entry(borrowHistory).State = EntityState.Modified;
                db.SaveChanges();
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
