namespace Marketplace.Extensions
{
	public static class EnvironmentExtensions
	{
		public static bool IsUAT(this IHostEnvironment hostEnvironment)
		{
			return hostEnvironment.IsEnvironment("UAT");
		}
	}
}
