using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trasher.BLL.Implementations;
using Trasher.BLL.Interfaces;
using Trasher.DAL.Repositories.Implementations;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.DAL.SqlServer;
using Trasher.Domain.Common;
using Trasher.Domain.Entities.Orders;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connection));

builder.Services.AddScoped<IBaseRepository<Order>, BaseRepository<Order>>();
//builder.Services.AddScoped<IBaseRepository<Review>, BaseRepository<Review>>();
builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<IReviewService, ReviewService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

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
