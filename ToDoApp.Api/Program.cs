using ToDoApp.Infrastracture.Extentions;
using ToDoApp.Infrastracture.Seeder;
using ToDoApp.Application.Extentions;
using Microsoft.OpenApi.Models;
using ToDoApp.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
    config.MapType<DateTime>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date-time"
    });
    config.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            },
            []
        }
    });
    }
);
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddAplication(builder.Configuration);

var app = builder.Build();
var scope = app.Services.CreateScope();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var seeder = scope.ServiceProvider.GetRequiredService<IToDoAppSeeder>();
    await seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
