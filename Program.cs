using NLog.Web;
using RestaurantAPI;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<RestaurantDbContext>();

builder.Services.AddScoped<RestaurantSeeder>();

builder.Host.UseNLog();

builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
    seeder.Seed();
}


app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
