using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
using WebAppServer1.Data;

namespace WebAppServer1.Controllers
{
    public class UserFsController : Controller
    {
        private readonly WebAppServer1Context _context;

        public UserFsController(WebAppServer1Context context)
        {
            _context = context;
        }

        // GET: UserFs
        public async Task<IActionResult> Index()
        {
              return View(await _context.UserF.ToListAsync());
        }

        // GET: UserFs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.UserF == null)
            {
                return NotFound();
            }

            var userF = await _context.UserF
                .FirstOrDefaultAsync(m => m.Username == id);
            if (userF == null)
            {
                return NotFound();
            }

            return View(userF);
        }

        // GET: UserFs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserFs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,NickName,Image,Server")] UserF userF)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userF);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userF);
        }

        // GET: UserFs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UserF == null)
            {
                return NotFound();
            }

            var userF = await _context.UserF.FindAsync(id);
            if (userF == null)
            {
                return NotFound();
            }
            return View(userF);
        }

        // POST: UserFs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,NickName,Image,Server")] UserF userF)
        {
            if (id != userF.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userF);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFExists(userF.Username))
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
            return View(userF);
        }

        // GET: UserFs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.UserF == null)
            {
                return NotFound();
            }

            var userF = await _context.UserF
                .FirstOrDefaultAsync(m => m.Username == id);
            if (userF == null)
            {
                return NotFound();
            }

            return View(userF);
        }

        // POST: UserFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.UserF == null)
            {
                return Problem("Entity set 'WebAppServer1Context.UserF'  is null.");
            }
            var userF = await _context.UserF.FindAsync(id);
            if (userF != null)
            {
                _context.UserF.Remove(userF);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFExists(string id)
        {
          return _context.UserF.Any(e => e.Username == id);
        }
    }
}
