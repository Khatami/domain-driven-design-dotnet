namespace Framework.Query.Attributes
{
	public class StreamingAttribute : Attribute
	{
		public StreamingAttribute(params string[] streams)
		{
			Streams = streams.ToList();
		}

		public List<string> Streams { get; set; }
    }
}
