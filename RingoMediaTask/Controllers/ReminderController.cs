using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Models;

namespace RingoMediaTask.Controllers
{
    public class ReminderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ReminderController(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _appDbContext.Reminders.ToListAsync());
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _appDbContext.Reminders
                .FirstOrDefaultAsync(m => m.ReminderId == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReminderId,Title,Description,ReminderDate")] Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Add(reminder);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _appDbContext.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReminderId,Title,Description,ReminderDate")] Reminder reminder)
        {
            if (id != reminder.ReminderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(reminder);
                    await _appDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReminderExists(reminder.ReminderId))
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
            return View(reminder);
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _appDbContext.Reminders
                .FirstOrDefaultAsync(m => m.ReminderId == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reminder = await _appDbContext.Reminders.FindAsync(id);
            _appDbContext.Reminders.Remove(reminder);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReminderExists(int id)
        {
            return _appDbContext.Reminders.Any(e => e.ReminderId == id);
        }
    }
}
