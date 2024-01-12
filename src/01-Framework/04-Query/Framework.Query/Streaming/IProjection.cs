namespace Framework.Query.Streaming
{
	public interface IProjection
	{
		Task Project(object @event);
	}
}
