using LibraryManagementUI.DAL;
using LibraryManagementUI.Models;
using System;
using System.Collections.Generic;
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
            return View(db.Books.ToList());
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
    }
}