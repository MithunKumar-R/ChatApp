using Microsoft.EntityFrameworkCore;
using TestDevelopment;
using TestDevelopment.Database;
using TestDevelopment.DataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDapperConnection, DapperDatabase>();
builder.Services.AddSingleton<SharedDb>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000", "http://192.168.29.197:3000")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
});
app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");
app.Run();

