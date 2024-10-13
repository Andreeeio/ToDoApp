using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Infrastracture.Presistance;
using ToDoApp.Infrastracture.Seeder;

namespace ToDoApp.Infrastracture.Extentions;

public static class ServiceCollectionExtention
{
    public static void AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ToDoApp");
        services.AddDbContext<ToDoAppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IToDoAppSeeder, ToDoAppSeeder>();
    }
}
