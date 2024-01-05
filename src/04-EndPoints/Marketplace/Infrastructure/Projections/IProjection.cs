namespace Marketplace.Infrastructure.Projections
{
	public interface IProjection
	{
		Task Project(object @event);
	}
}
