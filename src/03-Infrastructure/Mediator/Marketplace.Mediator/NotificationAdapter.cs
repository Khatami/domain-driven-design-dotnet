using MediatR;

namespace Marketplace.Mediator
{
	internal class NotificationAdapter<TNotification> : INotification
	{
		public NotificationAdapter(TNotification value)
		{
			Value = value;
		}

		public TNotification Value { get; }
	}
}