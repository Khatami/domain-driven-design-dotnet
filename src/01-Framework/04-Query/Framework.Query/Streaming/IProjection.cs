namespace Framework.Query.Streaming
{
	public interface IProjection
	{
		Task Project(object @event, string stream, long eventNumberInStream, long version);
	}
}
