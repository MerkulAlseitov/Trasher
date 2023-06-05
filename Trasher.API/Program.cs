using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trasher.BLL.Implementations;
using Trasher.BLL.Interfaces;
using Trasher.DAL.Repositories.Implementations;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.DAL.SqlServer;
using Trasher.Domain.Common;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Users;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connection), ServiceLifetime.Scoped);

builder.Services.AddScoped<IBaseRepository<Order>, BaseRepository<Order>>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddIdentity<Client, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserStore<Client>, UserStore<Client, IdentityRole, AppDbContext, string>>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();