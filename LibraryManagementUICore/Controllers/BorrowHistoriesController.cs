using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementUICore.DAL;
using LibraryManagementUICore.Models;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementUICore.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private readonly LibraryDbContext _context;

        public BorrowHistoriesController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: BorrowHistories
        public async Task<IActionResult> Index()
        {
            var libraryDbContext = _context.BorrowHistories.Include(b => b.book).Include(b => b.customer);
            return View(await libraryDbContext.ToListAsync());
        }

        // GET: BorrowHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowHistories
                .Include(b => b.book)
                .Include(b => b.customer)
                .FirstOrDefaultAsync(m => m.BorrowHistoryId == id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            return View(borrowHistory);
        }

        // GET: BorrowHistories/Create
        public IActionResult Create(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            var borrowHistory = new BorrowHistory
            {
                BookId = book.BookId,
                BorrowDate = DateTime.Now
            };
            //ViewData["BookId"] = new SelectList(_context.Books, "BookId", "SerialNumber");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BookId"] = new SelectList(_context.Books, "BookId", "SerialNumber", borrowHistory.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", borrowHistory.CustomerId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Edit/5
        public IActionResult Edit(int? id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            //where LINQ
            var borrowHistory = _context.BorrowHistories
                                    .Include(b => b.book)
                                    .Include(b => b.customer)
                                    .Where(b => b.BookId == id && b.ReturnDate == null)
                                        .FirstOrDefault();
            if (borrowHistory == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            //ViewData["BookId"] = new SelectList(_context.Books, "BookId", "SerialNumber", borrowHistory.BookId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", borrowHistory.CustomerId);
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    borrowHistory.ReturnDate = DateTime.Now;
                    _context.Update(borrowHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowHistoryExists(borrowHistory.BorrowHistoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BookId"] = new SelectList(_context.Books, "BookId", "SerialNumber", borrowHistory.BookId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", borrowHistory.CustomerId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowHistories
                .Include(b => b.book)
                .Include(b => b.customer)
                .FirstOrDefaultAsync(m => m.BorrowHistoryId == id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            return View(borrowHistory);
        }

        // POST: BorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowHistory = await _context.BorrowHistories.FindAsync(id);
            _context.BorrowHistories.Remove(borrowHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowHistoryExists(int id)
        {
            return _context.BorrowHistories.Any(e => e.BorrowHistoryId == id);
        }
    }
}
