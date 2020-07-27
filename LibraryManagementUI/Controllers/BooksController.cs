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
    public class BooksController : Controller
    {
        LibraryDBContext db = new LibraryDBContext();

        // GET: Books
        public ActionResult Index()
        {
            //eager loading + navigation properties
            var books = db.Books.Include(b => b.BorrowHistories);
            // select linq
            var availableBooks = books.Select(book => new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                SerialNumber = book.SerialNumber,
                IsAvailable = !book.BorrowHistories.Any(h => h.ReturnDate == null)
            }).ToList();
            
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("key1", "val1");
            //var value = dict["key1"];

            //List<Dictionary<string, string>> list_of_dicts = new List<Dictionary<string, string>>();

            
            return View(availableBooks);
        }

        //Get: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if(book == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}