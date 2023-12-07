using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data;
using phone_book_app.Server.ExceptionHandlers;
using phone_book_app.Server.InputModels;
using phone_book_app.Server.Policies;
using phone_book_app.Server.Repositories;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.Services;
using phone_book_app.Server.Services.Contracts;
using phone_book_app.Server.UnitOfWorks;
using phone_book_app.Server.UnitOfWorks.Contracts;
using phone_book_app.Server.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<PhoneBookAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPhoneBookAppUnitOfWork, PhoneBookAppUnitOfWork>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ILabelRepository, LabelRepository>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ILabelService, LabelService>();

builder.Services.AddExceptionHandler<AppExceptionHandler>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IValidator<ContactInputModel>, ContactInputModelValidator>();
builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
builder.Services.AddFluentValidationClientsideAdapters(); // for client side

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

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
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

app.UseExceptionHandler(_ => { });

app.Run();
