namespace Marketplace.Application.Infrastructure.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
