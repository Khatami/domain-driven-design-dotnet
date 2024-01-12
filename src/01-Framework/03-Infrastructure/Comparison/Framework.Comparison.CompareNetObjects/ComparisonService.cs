using Framework.Domain.Comparison;
using KellermanSoftware.CompareNetObjects;

namespace Framework.Comparison.CompareNetObjects
{
	internal class ComparisonService : IComparisonService
	{
		public bool Compare<T, W>(T first, W second)
		{
			CompareLogic compareLogic = new CompareLogic();

			ComparisonResult result = compareLogic.Compare(first, second);

			return result.AreEqual;
		}
	}
}
