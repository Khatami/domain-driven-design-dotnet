namespace Marketplace.Persistence.EventStore
{
	public sealed partial class EventData
	{

		public EventData(Guid eventId, string type, bool isJson, byte[] data, byte[] metadata)
		{
			EventId = eventId;
			Type = type;
			IsJson = isJson;
			Data = data;
			Metadata = metadata;
		}

		public readonly Guid EventId;

		public readonly string Type;

		public readonly bool IsJson;

		public readonly byte[] Data;

		public readonly byte[] Metadata;
	}
}
