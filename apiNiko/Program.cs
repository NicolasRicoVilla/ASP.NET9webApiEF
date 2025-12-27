using apiNiko.Data;
using apiNiko.Dtos;
using apiNiko.Models;
using Microsoft.EntityFrameworkCore;
using static apiNiko.Dtos.TaskDtos;
using static apiNiko.Dtos.UserDtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Seed 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

// GET /users 
app.MapGet("/users", async (AppDbContext db) =>
{
    var users = await db.Users
        .Select(u => new UserSummaryDtos(
            u.Id,
            u.Name,
            u.Age,
            u.Tasks.Count(t => !t.IsDone)
        ))
        .ToListAsync();

    return Results.Ok(users);
});

// POST /users
app.MapPost("/users", async (CreateUserDto dto, AppDbContext db) =>
{
    var user = new User { Name = dto.Name.Trim(), Age = dto.Age };
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", new { user.Id, user.Name, user.Age });
});

// GET /users/{userId}/tasks
app.MapGet("/users/{userId:int}/tasks", async (int userId, AppDbContext db) =>
{
    var exists = await db.Users.AnyAsync(u => u.Id == userId);
    if (!exists) return Results.NotFound(new { error = "User not found" });

    var tasks = await db.Tasks
        .Where(t => t.UserId == userId)
        .Select(t => new TaskDto(t.Id, t.Title, t.IsDone))
        .ToListAsync();

    return Results.Ok(tasks);
});

// POST /users/{userId}/tasks
app.MapPost("/users/{userId:int}/tasks", async (int userId, CreateTaskDto dto, AppDbContext db) =>
{
    var exists = await db.Users.AnyAsync(u => u.Id == userId);
    if (!exists) return Results.NotFound(new { error = "User not found" });

    var task = new TodoTask { UserId = userId, Title = dto.Title.Trim(), IsDone = false };
    db.Tasks.Add(task);
    await db.SaveChangesAsync();

    return Results.Created($"/tasks/{task.Id}", new TaskDto(task.Id, task.Title, task.IsDone));
});

// PATCH /tasks/{id}/done
app.MapPatch("/tasks/{id:int}/done", async (int id, AppDbContext db) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null) return Results.NotFound(new { error = "Task not found" });

    task.IsDone = true;
    await db.SaveChangesAsync();

    return Results.Ok(new TaskDto(task.Id, task.Title, task.IsDone));
});

app.Run();
