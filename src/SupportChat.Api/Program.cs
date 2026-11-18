
using Microsoft.EntityFrameworkCore;
using SupportChat.Api.Data;
using SupportChat.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register SQLite database
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlite(connectionString));

// Register SignalR
builder.Services.AddSignalR();

// Register OpenAPI and Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map REST API controllers
app.MapControllers();

// Map SignalR Hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
