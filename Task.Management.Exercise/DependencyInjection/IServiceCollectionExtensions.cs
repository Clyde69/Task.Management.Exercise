using TaskManagement.Exercise.Database.Contexts;
using TaskManagement.Exercise.Database.Repositories;

namespace TaskManagement.Exercise.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
        {
            services.AddScoped<TaskManagementDbContext>();
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<TaskRepository>();

            return services;
        }
    }
}
