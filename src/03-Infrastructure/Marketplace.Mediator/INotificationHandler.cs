namespace Marketplace.Mediator;

public interface INotificationHandler<TNotification> :
	MediatR.INotificationHandler<TNotification> where TNotification : INotification
{
}
