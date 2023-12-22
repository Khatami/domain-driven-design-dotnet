using Autofac;
using Autofac.Extensions.DependencyInjection;
using Marketplace.Application.Extensions;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Extensions;
using Marketplace.Infrastructure;
using Marketplace.Persistence.EF.Extensions;
using Marketplace.Persistence.EventStore.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

var persistenceApproach = builder.Configuration.GetValue<PersistenceApproach>("PersistenceApproach");

switch (persistenceApproach)
{
	case PersistenceApproach.RavenDB:
		builder.Services.AddRavenDBServices(builder.Configuration);
		break;
	case PersistenceApproach.EntityFramework:
		builder.Services.AddEFServices(builder.Configuration);
		break;
	case PersistenceApproach.EventStore:
		builder.Services.AddEventStoreServices(builder.Configuration);
		break;
	default:
		break;
}

builder.Services.AddEdgeServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	var openTypes = new[]
	{
		typeof(ICommandHandler<,>),
		typeof(ICommandHandler<>),
		typeof(IQueryHandler<,>)
	};

	List<Assembly> assemblies = new List<Assembly>()
	{
		typeof(Marketplace.Application.Extensions.ServiceExtensions).Assembly
	};

	switch (persistenceApproach)
	{
		case PersistenceApproach.RavenDB:
			assemblies.Add(typeof(Marketplace.Queries.RavenDB.AppInfo).Assembly);
			break;
		case PersistenceApproach.EntityFramework:
			assemblies.Add(typeof(Marketplace.Queries.EF.AppInfo).Assembly);
			break;
		case PersistenceApproach.EventStore:
			break;
		default:
			break;
	}

	foreach (var openType in openTypes)
	{
		builder
			.RegisterAssemblyTypes(assemblies.ToArray())
			.AsClosedTypesOf(openType)
			.AsImplementedInterfaces()
			.InstancePerLifetimeScope();
	}
});

var app = builder.Build();

if (persistenceApproach == PersistenceApproach.EntityFramework)
{
	app.EnsureDatabaseCreated();
}

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
