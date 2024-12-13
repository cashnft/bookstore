using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using OnlineBookstore.Infrastructure.Cache;
using OnlineBookstore.Domain.Interfaces;
using OnlineBookstore.Application.Services;  // Add this

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BookService>();
// Add DbContext with Npgsql
builder.Services.AddScoped<OrderService>();
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Redis and Cache Service BEFORE builder.Build()
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379"));
builder.Services.AddScoped<ICacheService, RedisCacheService>();

// Build the application AFTER registering all services
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

// Test endpoint
app.MapGet("/authors", async (BookstoreContext db) =>
    await db.Authors.ToListAsync());

app.Run();