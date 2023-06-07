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

builder.Services.AddScoped<IBaseRepository<Order>>(provider => new BaseRepository<Order>(provider.GetService<AppDbContext>()));
builder.Services.AddScoped<IBaseRepository<Review>>(provider => new BaseRepository<Review>(provider.GetService<AppDbContext>()));

//builder.Services.AddScoped<IBaseRepository<Order>, BaseRepository<Order>>();
//builder.Services.AddScoped<IBaseRepository<Review>, BaseRepository<Review>>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<IBrigadeService, BrigadeService>();



builder.Services.AddIdentity<Client, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserStore<Client>, UserStore<Client, IdentityRole, AppDbContext, string>>();
builder.Services.AddScoped<IUserStore<Operator>, UserStore<Operator, IdentityRole, AppDbContext, string>>();
builder.Services.AddScoped<IUserStore<Brigade>, UserStore<Brigade, IdentityRole, AppDbContext, string>>();

builder.Services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();
builder.Services.AddScoped<IPasswordHasher<Operator>, PasswordHasher<Operator>>();
builder.Services.AddScoped<IPasswordHasher<Brigade>, PasswordHasher<Brigade>>();


builder.Services.AddScoped<UserManager<Client>>();
builder.Services.AddScoped<UserManager<Operator>>();
builder.Services.AddScoped<UserManager<Brigade>>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultIdentity<Client>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


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