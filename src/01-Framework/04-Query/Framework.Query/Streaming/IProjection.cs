namespace Framework.Query.Streaming
{
	public interface IProjection
	{
		IEnumerable<string> Streams { get; }
		Task Project(object @event);
	}
}
