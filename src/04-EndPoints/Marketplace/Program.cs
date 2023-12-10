using Autofac;
using Autofac.Extensions.DependencyInjection;
using Marketplace.Application.Extensions;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Extensions;
using Marketplace.Persistence.EF.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

// TODO: check Persistence
var persistenceLayer = builder.Configuration.GetValue<long>("Persistence");

if (persistenceLayer == 0)
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
		typeof(IQueryHandler<,>),
	};

	foreach (var openType in openTypes)
	{
		builder
			.RegisterAssemblyTypes(typeof(Marketplace.Application.Extensions.ServiceExtensions).Assembly, 
				typeof(Marketplace.Query.ClassifiedAd.Models.ClassifiedAdDetails).Assembly)
			.AsClosedTypesOf(openType)
			.AsImplementedInterfaces();
	}
});

var app = builder.Build();

if (persistenceLayer == 1)
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
