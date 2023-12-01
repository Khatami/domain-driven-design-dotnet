namespace Marketplace.Application.Infrastructure
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
