using Microsoft.EntityFrameworkCore;
using apiNiko.Models;
namespace apiNiko.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

        public DbSet<User> Users => Set<User>();
        public DbSet<TodoTask> Tasks => Set<TodoTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User!)
                .HasForeignKey(t => t.UserId);
        }
    }
}
