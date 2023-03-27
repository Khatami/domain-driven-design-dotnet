using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Contracts.ClassifiedAds.IServices;
using Marketplace.Application.Helpers;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.Services
{
	/// <summary>
	/// It's completely against SRP
	/// </summary>
	internal class ClassifiedAdService : IClassifiedAdService
	{
		private readonly IEntityStore _entityStore;
		private readonly ICurrencyLookup _currencyLookup;

		public ClassifiedAdService(IEntityStore entityStore, ICurrencyLookup currencyLookup)
		{
			_entityStore = entityStore;
			_currencyLookup = currencyLookup;
		}

		public async Task Handle(object command)
		{
			ClassifiedAd classifiedAd;

			switch (command)
			{
				case CreateClassifiedAd_V1 cmd:
					var exists = await _entityStore.Exists<ClassifiedAd>(cmd.Id.ToString());

					if (exists)
						throw new InvalidOperationException($"Entitywith id {cmd.Id} already exists");

					classifiedAd = new ClassifiedAd(new ClassifiedAdId(cmd.Id), new UserId(cmd.OwnerId));

					await _entityStore.Save(classifiedAd);
					break;

				case SetClassifiedAdTitle_V1 cmd:
					classifiedAd = await _entityStore.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));

					await _entityStore.Save(classifiedAd);
					break;

				case UpdateClassifiedAdText_V1 cmd:
					classifiedAd = await _entityStore.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.UpdateText(ClassifiedAdText.FromString(cmd.Text));

					await _entityStore.Save(classifiedAd);
					break;

				case UpdateClassifiedAdPrice_V1 cmd:
					classifiedAd = await _entityStore.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(cmd.Price, cmd.Currency, _currencyLookup)));

					await _entityStore.Save(classifiedAd);
					break;

				case RequestClassifiedAdToPublish_V1 cmd:
					classifiedAd = await _entityStore.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.RequestToPublish();

					await _entityStore.Save(classifiedAd);
					break;

				default:
					throw new InvalidOperationException($"Command type {command.GetType().FullName} is unkown");
			}
		}
	}
}
