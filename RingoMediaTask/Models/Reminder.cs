namespace RingoMediaTask.Models
{
    public class Reminder
    {
        public int ReminderId { get; set; }
        public string ReminderTitle { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public bool IsSent { get; set; }
    }
}
