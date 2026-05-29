using BikeShop.Common;
using BikeShop.Infrastructure;
using BikeShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext PostgreSQL
builder.Services.AddDbContext<BikeShopContext>();

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// CRUD Service з BikeShop.Common
builder.Services.AddScoped(typeof(CrudService<>));

var app = builder.Build();

// автоматичне застосування міграцій
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BikeShopContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();