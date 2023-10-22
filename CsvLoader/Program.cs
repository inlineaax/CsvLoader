using Application.IServices;
using Application.Services;
using Domain.IRepositories;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CSV Data Loader API", Version = "v1" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "CsvLoader.xml");
    c.IncludeXmlComments(filePath);
});


//DB
builder.Services.AddDbContext<CsvLoaderContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Services
builder.Services.AddTransient<ICsvLoaderService, CsvLoaderService>();

//Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();

//Interfaces
builder.Services.AddTransient<ICsvLoaderContext, CsvLoaderContext>();

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

app.Run();
