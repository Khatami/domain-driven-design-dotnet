using Autofac.Extensions.DependencyInjection;
using Framework.BackgroundJob.Hangfire.MSSQL.Extensions;
using Framework.Comparison.CompareNetObjects.Extensions;
using Framework.Streaming.EventStore.Extensions;
using Marketplace.Api.Extensions;
using Marketplace.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddStreamingServices(builder.Configuration);
builder.Services.AddComparisonServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddEdgeServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.AddCQRSServices();
builder.Host.AddProjectionServices();

var app = builder.Build();

app.UsePathBase("/marketplace");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsUAT())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.EnsureDatabaseCreated(builder.Configuration);

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.MapControllers();

app.UseHangfireMiddlewares();

app.StartProjections();

app.Run();