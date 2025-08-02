using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Entities;

namespace TaskList.Infrastructure.Persistence;

public class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("Tarefas");
            entity.HasKey(t => t.Id);
            entity.HasIndex(t => t.Name).IsUnique();
            entity.HasIndex(t => t.DisplayOrder).IsUnique();

            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Cost).HasColumnType("decimal(10,2)");
            entity.Property(t => t.DueDate).IsRequired();
            entity.Property(t => t.DisplayOrder).IsRequired();
        });
    }
}
