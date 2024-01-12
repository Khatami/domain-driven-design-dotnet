using Framework.Domain.Comparison;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Comparison.CompareNetObjects.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddComparisonServices
		(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
	{
		services.AddTransient<IComparisonService, ComparisonService>();
	}
}
