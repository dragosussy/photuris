using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext;
using photuris_backend.Utilities.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection.
builder.Services.AddDbContext<Repository>(options =>
   options.UseSqlServer(builder.Configuration.GetSection("LocalDbConnectionString").Value));
builder.Services.AddScoped<UsersManager>();
// builder.Services.AddHostedService<ExpiredSessionsCleanerJob>(); TODO uncomment this

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
    options.WithOrigins("http://localhost:8081/", "http://192.168.0.193:8081/")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
