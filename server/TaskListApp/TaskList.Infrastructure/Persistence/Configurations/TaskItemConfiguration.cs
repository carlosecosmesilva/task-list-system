using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskList.Domain.Entities;

namespace TaskList.Infrastructure.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tarefas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Cost)
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.DisplayOrder)
                .IsRequired();

            // REMOVER: Índice único no DisplayOrder
            // builder.HasIndex(t => t.DisplayOrder).IsUnique();

            // MANTER: Índice único no Name
            builder.HasIndex(t => t.Name).IsUnique();

            // ADICIONAR: Índice não-único para performance
            builder.HasIndex(t => t.DisplayOrder);
        }
    }
}