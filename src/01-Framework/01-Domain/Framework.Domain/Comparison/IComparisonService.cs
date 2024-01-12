namespace Framework.Domain.Comparison
{
	public interface IComparisonService
	{
		bool Compare<T, W>(T first, W second);
	}
}