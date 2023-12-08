using MediatR;

namespace Marketplace.Mediator
{
	public class NotificationAdapter<TNotification> : INotification
	{
		public NotificationAdapter(TNotification value)
		{
			Value = value;
		}

		public TNotification Value { get; }
	}
}