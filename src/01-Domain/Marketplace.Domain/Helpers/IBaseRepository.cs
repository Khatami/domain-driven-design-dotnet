namespace Marketplace.Domain.Helpers
{
	public interface IBaseRepository
	{
		Task<T> Load<T>(string id);

		Task Save<T>(T entity);

		Task<bool> Exists<T>(string id);
	}
}
