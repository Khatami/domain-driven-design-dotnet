using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers
{
	internal class UpdateClassifiedAdCommandHandler : ICommandHandler<UpdateClassifiedAdCommand>
	{
		private readonly IAggregateStore _aggregateStore;
		private readonly ICurrencyLookup _currencyLookup;
		public UpdateClassifiedAdCommandHandler(IAggregateStore aggregateStore, ICurrencyLookup currencyLookup)
		{
			_aggregateStore = aggregateStore;
			_currencyLookup = currencyLookup;
		}

		public async Task Handle(UpdateClassifiedAdCommand request, CancellationToken cancellationToken)
		{
			var classifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

			if (classifiedAd == null)
			{
				throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
			}

			classifiedAd.SetTitle(ClassifiedAdTitle.FromString(request.Title));
			classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(request.Price, request.Currency, _currencyLookup)));
			classifiedAd.UpdateText(ClassifiedAdText.FromString(request.Text));

			await _aggregateStore.Save<ClassifiedAd, ClassifiedAdId>(classifiedAd, cancellationToken);
		}
	}
}
