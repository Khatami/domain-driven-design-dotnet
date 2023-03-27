namespace Marketplace.Application.Helpers
{
	public interface IEntityStore
	{
		Task<T> Load<T>(string id);

		Task Save<T>(T entity);

		Task<bool> Exists<T>(string id);
	}
}
