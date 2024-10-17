using Labb1_ResturantBookingSystem.Data;
using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Data.Repos;
using Labb1_ResturantBookingSystem.Services.IServices;
using Labb1_ResturantBookingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Labb1RestaurantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseCors("LocalReact");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


