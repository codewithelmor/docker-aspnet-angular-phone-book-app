using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using phone_book_app.Server.Data;
using phone_book_app.Server.Policies;
using phone_book_app.Server.Services;
using phone_book_app.Server.Services.Contracts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<PhoneBookAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ILabelService, LabelService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    string[] origins = new[] { "http://localhost:4200", "https://localhost:4200" };

    options.AddPolicy(ControllerPolicy.Cors,
        builder => builder
            .WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
