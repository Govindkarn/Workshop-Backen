using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;
public static class DependencyInjection
{
    public static IServiceCollection AddData(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        string ConnecttionString = configuration
            .GetConnectionString("Database") 
            ?? throw new ArgumentNullException("Connection string is null");

        services.AddDbContext<ApplicationDbContext>(
            options => 
                options.UseNpgsql(ConnecttionString));

        services.AddScoped<ITodoRepository, TodoRepository>();
        
        return services;
    }
}
