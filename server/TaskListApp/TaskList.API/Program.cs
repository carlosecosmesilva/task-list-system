using Microsoft.EntityFrameworkCore;
using TaskList.Application.Services;
using TaskList.Domain.Repositories;
using TaskList.Infrastructure.Persistence;
using TaskList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar porta do Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar conexão com banco
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseNpgsql(connectionString));

// DI
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Configurar CORS - ADICIONAR ANTES DE builder.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ADICIONAR LOGO APÓS app = builder.Build()
app.UseCors("AllowFrontend");

// Configure pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Auto-migrate no Railway
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    try
    {
        context.Database.Migrate();
        Console.WriteLine("✅ Database migrated successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database migration failed: {ex.Message}");
    }
}

app.Run();

// Helper para converter URL do PostgreSQL
static string ConvertPostgresUrlToConnectionString(string postgresUrl)
{
    var uri = new Uri(postgresUrl);
    var userInfo = uri.UserInfo.Split(':');

    return $"Host={uri.Host};Port={uri.Port};Database={uri.LocalPath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
}