using Microsoft.EntityFrameworkCore;
using OnlineOrderManagement.Infrastructure.Data;
using OnlineOrderManagement.Infrastructure.Repositories;
using OnlineOrderManagement.Application;
using OnlineOrderManagement.Application.Services;
using OnlineOrderManagement.Domain.Repositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Net;
using System;
using System.Text.Json.Serialization;
using OnlineOrderManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers()
  .AddJsonOptions(opts =>
    opts.JsonSerializerOptions.ReferenceHandler
       = ReferenceHandler.IgnoreCycles
  );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register UnitOfWork and Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
