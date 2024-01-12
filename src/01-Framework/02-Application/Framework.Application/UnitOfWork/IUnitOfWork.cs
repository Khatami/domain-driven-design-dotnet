namespace Framework.Application.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task Commit(CancellationToken cancellationToken);
	}
}
