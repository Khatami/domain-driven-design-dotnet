using Autofac.Extensions.DependencyInjection;
using Marketplace.Application.Extensions;
using Marketplace.BackgroundJob.Hangfire.MSSQL.Extensions;
using Marketplace.Extensions;
using Marketplace.Infrastructure;
using Marketplace.Persistence.EF.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;
using Marketplace.Streaming.EventStore.Extensions;
using Marketplace.Comparison.CompareNetObjects.Extensions;
using Marketplace.Infrastructure.Subscribtions.Infrastructure;
using Marketplace.Infrastructure.Subscribtions.Projections;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;
using Marketplace.Queries.Contracts.ReadModels.UserProfiles;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddEventStoreServices(builder.Configuration);
builder.Services.AddComparisonServices();

//********************************************
// Temperoray
//********************************************
builder.Services.AddSingleton<ProjectionManager>();
//********************************************
//********************************************

var persistenceApproach = builder.Configuration
	.GetSection("ServiceSettings")
	.GetValue<PersistenceApproach>("PersistenceApproach");

var QueryApproach = builder.Configuration
	.GetSection("ServiceSettings")
	.GetValue<PersistenceApproach>("QueryApproach");

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

builder.Host.AddAutofacServices(persistenceApproach);

var app = builder.Build();

//********************************************
// Temperoray
//********************************************
var esSubscribtion = app.Services.GetRequiredService<ProjectionManager>();

esSubscribtion.Start(new ClassifiedAdDetailsProjection(), new UserDetailsProjection());
//********************************************
//********************************************

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
