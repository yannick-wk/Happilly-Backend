using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Happilly_backend.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HappillyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Happilly_backendContext") ?? throw new InvalidOperationException("Connection string 'Happilly_backendContext' not found.")));

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.Run();
