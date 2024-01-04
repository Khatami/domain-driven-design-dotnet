using EventStore.Client;
using Marketplace.Domain.ClassifiedAds.Events;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;
using Marketplace.Streaming.EventStore.Metadata;
using System.Text;
using System.Text.Json;

namespace Marketplace.Infrastructure.Subscriptions
{
	//********************************************
	// Temperoray
	//********************************************
	public class EsSubscription
	{
		public static List<ClassifiedAdDetails> ClassifiedAdDetails = new();

		private readonly EventStoreClient _client;
		public EsSubscription(IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("EventStoreConnectionString");

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			var settings = EventStoreClientSettings.Create(connectionString);

			_client = new EventStoreClient(settings);
		}

		public void Start()
		{
 			_client.SubscribeToAllAsync(FromAll.Start, EventAppeared);
		}

		private Task EventAppeared(StreamSubscription subscription, 
			ResolvedEvent resolvedEvent,
			CancellationToken cancellationToken)
		{
			if (resolvedEvent.Event.EventType.StartsWith("$"))
				return Task.CompletedTask;

			var @event = ReadEvents(resolvedEvent);

			switch (@event)
			{
				case ClassifiedAdCreated e:
					ClassifiedAdDetails.Add(new ClassifiedAdDetails
					{
						ClassifiedAdId = e.Id
					});
					break;
				case ClassifiedAdTitleChanged e:
					UpdateItem(e.Id, ad => ad.Title = e.Title);
					break;
				case ClassifiedAdTextChanged e:
					UpdateItem(e.Id, ad => ad.Description = e.AdText);
					break;
				case ClassifiedAdPriceUpdated e:
					UpdateItem(e.Id, ad =>
					{
						ad.Price = e.Price;
						ad.CurrencyCode = e.CurrencyCode;
					});
					break;
			}

			return Task.CompletedTask;
		}

		private void UpdateItem(Guid id, Action<ClassifiedAdDetails> update)
		{
			var item = ClassifiedAdDetails.FirstOrDefault(x => x.ClassifiedAdId == id);

			if (item == null) return;

			update(item);
		}

		private object ReadEvents(ResolvedEvent resolvedEvent)
		{
			var metadata = Deserialize<EventMetadata>(resolvedEvent.Event.Metadata.ToArray());

			var dataType = Type.GetType(metadata.ClrType);
			if (dataType == null)
			{
				throw new ArgumentNullException(nameof(dataType));
			}

			var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());

			var data = JsonSerializer.Deserialize(jsonData, dataType);

			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return data;
		}

		private T Deserialize<T>(byte[] data)
		{
			string decodedData = Encoding.UTF8.GetString(data);

			var finalobject = JsonSerializer.Deserialize<T>(decodedData);

			if (finalobject == null)
			{
				throw new ArgumentNullException(nameof(finalobject));
			}

			return finalobject;
		}
	}
}