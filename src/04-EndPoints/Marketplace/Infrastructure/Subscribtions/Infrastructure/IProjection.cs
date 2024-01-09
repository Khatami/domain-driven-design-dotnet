namespace Marketplace.Infrastructure.Subscribtions.Infrastructure
{
    public interface IProjection
    {
        Task Project(object @event);
    }
}
