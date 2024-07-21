using Microsoft.EntityFrameworkCore;

namespace RingoMediaTask.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(dept => dept.ParentDepartment)
                .WithMany(dept => dept.SubDepartments)
                .HasForeignKey(dept => dept.ParentDepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        }
       
    }
}
