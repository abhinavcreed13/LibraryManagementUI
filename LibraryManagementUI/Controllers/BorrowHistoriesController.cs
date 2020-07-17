using LibraryManagementUI.DAL;
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
