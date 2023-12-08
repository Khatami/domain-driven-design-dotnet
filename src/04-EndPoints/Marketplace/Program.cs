using Autofac;
using Autofac.Extensions.DependencyInjection;
using Marketplace.Application.Extensions;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

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

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	var openTypes = new[]
	{
		typeof(ICommandHandler<,>),
		typeof(ICommandHandler<>)
	};

	foreach (var openType in openTypes)
	{
		builder
			.RegisterAssemblyTypes(typeof(Marketplace.Application.Extensions.ServiceExtensions).Assembly)
			.AsClosedTypesOf(openType)
			.AsImplementedInterfaces();
	}
});

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
