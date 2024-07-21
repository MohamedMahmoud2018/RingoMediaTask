using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Models;

namespace RingoMediaTask.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public ReminderService(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var reminders = await _context.Reminders
                    .Where(r => r.ReminderDateTime <= now && !r.IsSent)
                    .ToListAsync();

                foreach (var reminder in reminders)
                {
                    await _emailSender.SendEmailAsync("recipient@example.com", reminder.ReminderTitle, "Reminder notification");
                    reminder.IsSent = true;
                    _context.Reminders.Update(reminder);
                }

                await _context.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
