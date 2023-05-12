namespace Marketplace.Application.Shared
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
