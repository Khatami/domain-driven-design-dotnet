namespace Marketplace.Application.Helpers
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
