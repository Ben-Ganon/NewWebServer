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
    public class MessageChatsController : Controller
    {
        private readonly WebAppServer1Context _context;

        public MessageChatsController(WebAppServer1Context context)
        {
            _context = context;
        }

        // GET: MessageChats
        public async Task<IActionResult> Index()
        {
              return View(await _context.MessageChat.ToListAsync());
        }

        // GET: MessageChats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MessageChat == null)
            {
                return NotFound();
            }

            var messageChat = await _context.MessageChat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageChat == null)
            {
                return NotFound();
            }

            return View(messageChat);
        }

        // GET: MessageChats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageChats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Sent")] MessageChat messageChat)
        {
            if (ModelState.IsValid)
            {
                messageChat.Created = DateTime.Now;
                _context.Add(messageChat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageChat);
        }

        // GET: MessageChats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MessageChat == null)
            {
                return NotFound();
            }

            var messageChat = await _context.MessageChat.FindAsync(id);
            if (messageChat == null)
            {
                return NotFound();
            }
            return View(messageChat);
        }

        // POST: MessageChats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Sent")] MessageChat messageChat)
        {
            if (id != messageChat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    messageChat.Created = DateTime.Now;
                    _context.Update(messageChat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageChatExists(messageChat.Id))
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
            return View(messageChat);
        }


        // GET: MessageChats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MessageChat == null)
            {
                return NotFound();
            }

            var messageChat = await _context.MessageChat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageChat == null)
            {
                return NotFound();
            }

            return View(messageChat);
        }

        // POST: MessageChats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MessageChat == null)
            {
                return Problem("Entity set 'WebAppServer1Context.MessageChat'  is null.");
            }
            var messageChat = await _context.MessageChat.FindAsync(id);
            if (messageChat != null)
            {
                _context.MessageChat.Remove(messageChat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageChatExists(int id)
        {
          return _context.MessageChat.Any(e => e.Id == id);
        }
    }
}
