using apiNiko.Models;
using Microsoft.EntityFrameworkCore;
namespace apiNiko.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();
            if (await db.Users.AnyAsync()) return;

            var user = new User { Name = "Niko", Age = 25 };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            db.Tasks.AddRange(
                new TodoTask { UserId = user.Id, Title = "Leer documentación de C#", IsDone = false},
                new TodoTask { UserId = user.Id, Title = "Hacer prueba técnica", IsDone = false }
                );

            await db.SaveChangesAsync();
            
        }
    }
}
