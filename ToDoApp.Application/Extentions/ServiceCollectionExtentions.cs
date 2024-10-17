using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ToDoApp.Application.Users;
using ToDoApp.Application.Users.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;




namespace ToDoApp.Application.Extentions;

public static class ServiceCollectionExtentions
{
    public static void AddAplication(this IServiceCollection services) 
    { 
        services.AddScoped<IUserServices,UserServices>();

        services.AddAutoMapper(typeof(ServiceCollectionExtentions).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtentions).Assembly));

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtentions).Assembly).AddFluentValidationAutoValidation();

    }

}
