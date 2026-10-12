using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Exercise.Dtos;

namespace TaskManagement.Exercise.Database.EntityTypeConfigurations
{
    public class TaskDtoConfiguration : IEntityTypeConfiguration<TaskDto>
    {
        public void Configure(EntityTypeBuilder<TaskDto> builder)
        {
            builder.HasIndex(o => o.Id);

            builder.Property(o => o.Title)
                .IsRequired();
            builder.Property(o => o.CreatedAt)
                .IsRequired();
        }
    }
}
