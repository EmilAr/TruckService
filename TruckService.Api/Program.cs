using Microsoft.EntityFrameworkCore;
using TruckService.Api.Infrastructire.DatabaseContext;
using TruckService.Api.Infrastructire.Repositories;
using TruckService.Api.Model;
using TruckService.Api.Model.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TruckDbContext>(o =>
{
    o.UseInMemoryDatabase(databaseName: builder.Configuration.GetSection("TruckDatabase")["Name"]!);
});

// Add services to the container.
builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckService, TruckService.Api.Services.TruckService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(x => x.AddProfile(new AutoMapperProfile()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
