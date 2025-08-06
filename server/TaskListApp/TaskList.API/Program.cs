using Microsoft.EntityFrameworkCore;
using TaskList.Application.Services;
using TaskList.Domain.Repositories;
using TaskList.Infrastructure.Persistence;
using TaskList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os repositórios e serviços
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Adiciona o DbContext com PostgreSQL
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de CORS dinâmica baseada no ambiente
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Desenvolvimento: permite Angular local
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
        else
        {
            // Produção: configure com sua URL de produção
            policy.WithOrigins("https://your-production-domain.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
    });
});

// Configura o Swagger apenas em desenvolvimento
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TaskList API", Version = "v1" });
    });
}

// Adiciona os serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configurado o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Usa CORS antes do Authorization
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();