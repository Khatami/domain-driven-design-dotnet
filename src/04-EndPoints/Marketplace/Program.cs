using Marketplace.Application.Extensions;
using Marketplace.Extensions;
using Marketplace.Persistence.EF.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

// TODO: check Persistence
builder.Services.AddRavenDBServices();
//builder.Services.AddEFServices(builder.Configuration);

builder.Services.AddEdgeServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// TODO: check Persistence
//app.EnsureDatabase();

app.UsePathBase("/marketplace");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
