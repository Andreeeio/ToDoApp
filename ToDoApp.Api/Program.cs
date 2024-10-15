using ToDoApp.Infrastracture.Extentions;
using ToDoApp.Infrastracture.Seeder;
using ToDoApp.Application.Extentions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddAplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scope = app.Services.CreateScope();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var seeder = scope.ServiceProvider.GetRequiredService<IToDoAppSeeder>();
    await seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
