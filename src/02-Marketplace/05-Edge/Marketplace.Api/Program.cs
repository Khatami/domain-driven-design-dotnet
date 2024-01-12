using Autofac.Extensions.DependencyInjection;
using Framework.BackgroundJob.Hangfire.MSSQL.Extensions;
using Framework.Comparison.CompareNetObjects.Extensions;
using Framework.Streaming.EventStore.Extensions;
using Marketplace.Api.Extensions;
using Marketplace.Api.Infrastructure;
using Marketplace.Application.Extensions;
using Marketplace.Persistence.MSSQL.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddStreamingServices(builder.Configuration);
builder.Services.AddComparisonServices();

var persistenceApproach = builder.Configuration
	.GetSection("ServiceSettings")
	.GetValue<PersistenceApproach>("PersistenceApproach");

switch (persistenceApproach)
{
	case PersistenceApproach.RavenDB:
		builder.Services.AddRavenDBServices(builder.Configuration);
		break;
	case PersistenceApproach.EntityFramework:
		string? connectionString = builder.Configuration.GetConnectionString("SqlServerConnectionString");

		if (string.IsNullOrWhiteSpace(connectionString))
		{
			throw new ArgumentNullException(nameof(connectionString));
		}

		builder.Services.AddEFServices(builder.Configuration, connectionString);
		builder.Services.AddMSSQLHangfireServices(builder.Configuration, connectionString);
		break;
	default:
		throw new ArgumentOutOfRangeException(nameof(persistenceApproach));
}

builder.Services.AddEdgeServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.AddAutofacServices();

var app = builder.Build();

if (persistenceApproach == PersistenceApproach.EntityFramework)
{
	app.EnsureDatabaseCreated();
}

app.UsePathBase("/marketplace");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsUAT())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.MapControllers();

app.UseHangfireMiddlewares();

app.Run();
