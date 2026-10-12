using Microsoft.EntityFrameworkCore;
using TaskManagement.Exercise.Database.EntityTypeConfigurations;
using TaskManagement.Exercise.Dtos;

namespace TaskManagement.Exercise.Database.Contexts
{
    public class TaskManagementDbContext : DbContext
    {
        public virtual DbSet<TaskDto> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TaskManagement");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskDtoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
