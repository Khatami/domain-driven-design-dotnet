using Autofac;
using Autofac.Extensions.DependencyInjection;
using Marketplace.Application.Extensions;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Extensions;
using Marketplace.Persistence.EF.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

// TODO: check Persistence
var isRavenDbPerstistenceAndQuery = builder.Configuration.GetValue<long>("Persistence") == 0;

if (isRavenDbPerstistenceAndQuery)
{
	builder.Services.AddRavenDBServices();
}
else
{
	builder.Services.AddEFServices(builder.Configuration);
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
		typeof(ICommandResponse<>)
	};

	List<Assembly> assemblies = new List<Assembly>()
	{
		typeof(Marketplace.Application.Extensions.ServiceExtensions).Assembly
	};

	if (isRavenDbPerstistenceAndQuery)
	{
		assemblies.Add(typeof(Marketplace.Queries.RavenDB.AppInfo).Assembly);
	}
	else
	{
		assemblies.Add(typeof(Marketplace.Queries.EF.AppInfo).Assembly);
	}

	foreach (var openType in openTypes)
	{
		builder
			.RegisterAssemblyTypes(assemblies.ToArray())
			.AsClosedTypesOf(openType)
			.AsImplementedInterfaces();
	}
});

var app = builder.Build();

if (isRavenDbPerstistenceAndQuery == false)
{
	app.EnsureDatabase();
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
