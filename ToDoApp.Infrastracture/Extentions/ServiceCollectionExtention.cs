using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;
using ToDoApp.Infrastracture.Authorization;
using ToDoApp.Infrastracture.Presistance;
using ToDoApp.Infrastracture.Repositories;
using ToDoApp.Infrastracture.Seeder;

namespace ToDoApp.Infrastracture.Extentions;

public static class ServiceCollectionExtention
{
    public static void AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ToDoApp");
        services.AddDbContext<ToDoAppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IToDoAppSeeder, ToDoAppSeeder>();
        services.AddScoped<IUserRepositories,UserRepositories>();
        services.AddScoped<IAssignmentRepository,AssignmentRepository>();
        services.AddScoped<IAssignmentAuthorization,AssignmentAuthorization>();
        services.AddScoped<IUserAuthorizeServie,UserAuthorizeServie>();
    }
}
