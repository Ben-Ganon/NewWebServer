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
    public class ChatBoxesController : Controller
    {
        private readonly WebAppServer1Context _context;

        public ChatBoxesController(WebAppServer1Context context)
        {
            _context = context;
        }

        // GET: ChatBoxes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ChatBox.ToListAsync());
        }

        // GET: ChatBoxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChatBox == null)
            {
                return NotFound();
            }

            var chatBox = await _context.ChatBox
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatBox == null)
            {
                return NotFound();
            }

            return View(chatBox);
        }

        // GET: ChatBoxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChatBoxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] ChatBox chatBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatBox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatBox);
        }

        // GET: ChatBoxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChatBox == null)
            {
                return NotFound();
            }

            var chatBox = await _context.ChatBox.FindAsync(id);
            if (chatBox == null)
            {
                return NotFound();
            }
            return View(chatBox);
        }

        // POST: ChatBoxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] ChatBox chatBox)
        {
            if (id != chatBox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatBox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatBoxExists(chatBox.Id))
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
            return View(chatBox);
        }

        // GET: ChatBoxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChatBox == null)
            {
                return NotFound();
            }

            var chatBox = await _context.ChatBox
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatBox == null)
            {
                return NotFound();
            }

            return View(chatBox);
        }

        // POST: ChatBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChatBox == null)
            {
                return Problem("Entity set 'WebAppServer1Context.ChatBox'  is null.");
            }
            var chatBox = await _context.ChatBox.FindAsync(id);
            if (chatBox != null)
            {
                _context.ChatBox.Remove(chatBox);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatBoxExists(int id)
        {
          return _context.ChatBox.Any(e => e.Id == id);
        }
    }
}
