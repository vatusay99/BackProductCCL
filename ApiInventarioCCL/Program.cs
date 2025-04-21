using ApiInventarioCCL.Data;
using ApiInventarioCCL.ProductsMapper;
using ApiInventarioCCL.Repositories;
using ApiInventarioCCL.Repositories.Irepository;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseNpgsql(builder.Configuration.GetConnectionString("ConexionPgs"))
);

builder.Services.AddAutoMapper(typeof(ProductMapper));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

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

app.Run();
