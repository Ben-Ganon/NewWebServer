using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
//using WebAppServer1.Data;

namespace WebAppServer1.Controllers
{
    public class ReviewsController : Controller
    {

        public ReviewsController()
        {
       
        }

        

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            using (var db = new WebServerContext())
            {
                return View(await db.Reviews.ToListAsync());
            }
        }
        public async Task<IActionResult> Search()
        {
            using(var db = new WebServerContext())
            {
                return View(db.Reviews.ToListAsync());

            }
        }


        public async Task<IActionResult> Search2(string query)
        {
            using (var db = new WebServerContext())
            {
                var q = db.Reviews.Where(rev => rev.Title.Contains(query) || rev.Description.Contains(query)
          || rev.Name.Contains(query));
                return Json(await q.ToListAsync());
            }
              
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var db = new WebServerContext())
            {
                if (id == null || db.Reviews == null)
                {
                    return NotFound();
                }

                var review = await db.Reviews
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (review == null)
                {
                    return NotFound();
                }

                return View(review);
            }
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Stars,Name")] Review review)
        {
            using (var db = new WebServerContext())
            {
                if (ModelState.IsValid)
                {

                    review.Time = DateTime.Now;
                    db.Add(review);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(review);
            }
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using (var db = new WebServerContext())
            {
                if (id == null || db.Reviews == null)
                {
                    return NotFound();
                }

                var review = await db.Reviews.FindAsync(id);
                if (review == null)
                {
                    return NotFound();
                }
                return View(review);
            }
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Stars,Name")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }
            using (var db = new WebServerContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        review.Time = DateTime.Now;
                        db.Update(review);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReviewExists(review.Id))
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
                return View(review);
            }
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var db = new WebServerContext())
            {
                if (id == null || db.Reviews == null)
                {
                    return NotFound();
                }

                var review = await db.Reviews
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (review == null)
                {
                    return NotFound();
                }

                return View(review);
            }
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var db = new WebServerContext())
            {
                if (db.Reviews == null)
                {
                    return Problem("Entity set 'WebAppServer1Context.Review'  is null.");
                }
                var review = await db.Reviews.FindAsync(id);
                if (review != null)
                {
                    db.Reviews.Remove(review);
                }

                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }

            private bool ReviewExists(int id)
            {
            using (var db = new WebServerContext())
            {
                return db.Reviews.Any(e => e.Id == id);
            }
            }
        
    }
}
