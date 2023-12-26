namespace Marketplace.Application.SeedWork.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
