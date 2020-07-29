using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementUIMySQLEF6.DAL;
using LibraryManagementUIMySQLEF6.Models;

namespace LibraryManagementUIMySQLEF6.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: BorrowHistories
        public ActionResult Index()
        {
            var borrowHistories = db.BorrowHistories.Include(b => b.book).Include(b => b.customer);
            return View(borrowHistories.ToList());
        }

        // GET: BorrowHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            db.BorrowHistories.Remove(borrowHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
