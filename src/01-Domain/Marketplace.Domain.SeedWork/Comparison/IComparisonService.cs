namespace Marketplace.Domain.SeedWork.Comparison
{
	public interface IComparisonService
	{
		bool Compare<T, W>(T first, W second);
	}
}