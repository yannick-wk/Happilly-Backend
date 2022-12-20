using Happilly.Application.Configurations;
using Happilly.Application.Dtos;
using Happilly.Application.Interfaces;
using Happilly.Application.Services;
using Happilly.Domain.Entities;
using Happilly.Persistence.Database;
using Happilly.Persistence.Repositories;
using Happilly.Presentation.Mapping.Profiles;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


#region MAPPING LAYER
// The AutoMapper for Entities to DTOs and vice versa.
// Medicine -> MedicineDto (automatic mapping of entities and data transfer objects)
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MedicineProfile)));

// Logging to the console..
builder.Services.AddLogging(p => p.AddConsole());

// Add the database configuration (from appsettings.json)
builder.Services.AddOptions();
builder.Services.Configure<DbConfiguration>(builder.Configuration.GetSection("Database").Bind);

builder.Services.AddSingleton<IFactory<HappillyDbContext>, HappillyDatabaseFactory>();

// Instead of using .AddDbContext, .AddTransient is used because, the IFactory<HappillyDbContext>
// needs to be used for creating an instance of the HappillyDbContext.
builder.Services.AddTransient<HappillyDbContext>(p => p.GetService<IFactory<HappillyDbContext>>().Create());

// Adds the repository to the service collection
builder.Services.AddTransient<IRepository<Medicine>, MedicineRepository>(); // MedicineRepository

// Registers the MedicineService to the service collection.
builder.Services.AddTransient<IService<MedicineDto>, MedicineService>();

// Verifies the database connection upon start-up!
builder.Services.VerifyDatabaseConnection<HappillyDbContext>();
#endregion MAPPING LAYER

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.Run();
