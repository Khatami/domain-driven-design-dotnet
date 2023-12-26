using MediatR;

namespace Marketplace.Mediator.MediatR
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