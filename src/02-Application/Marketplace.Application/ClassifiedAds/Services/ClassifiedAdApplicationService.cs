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
	internal class ClassifiedAdApplicationService : IClassifiedAdApplicationService
	{
		private readonly IClassifiedAdRepository _classifiedAdRepository;
		private readonly ICurrencyLookup _currencyLookup;

		public ClassifiedAdApplicationService(IClassifiedAdRepository classifiedAdRepository, ICurrencyLookup currencyLookup)
		{
			_classifiedAdRepository = classifiedAdRepository;	
			_currencyLookup = currencyLookup;
		}

		public async Task Handle(object command)
		{
			ClassifiedAd classifiedAd;

			// Advanced pattern-matching
			switch (command)
			{
				case CreateClassifiedAd_V1 cmd:
					var exists = await _classifiedAdRepository.Exists<ClassifiedAd>(cmd.Id.ToString());

					if (exists)
						throw new InvalidOperationException($"Entitywith id {cmd.Id} already exists");

					classifiedAd = new ClassifiedAd(new ClassifiedAdId(cmd.Id), new UserId(cmd.OwnerId));

					await _classifiedAdRepository.Save(classifiedAd);
					break;

				case SetClassifiedAdTitle_V1 cmd:
					classifiedAd = await _classifiedAdRepository.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));

					await _classifiedAdRepository.Save(classifiedAd);
					break;

				case UpdateClassifiedAdText_V1 cmd:
					classifiedAd = await _classifiedAdRepository.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.UpdateText(ClassifiedAdText.FromString(cmd.Text));

					await _classifiedAdRepository.Save(classifiedAd);
					break;

				case UpdateClassifiedAdPrice_V1 cmd:
					classifiedAd = await _classifiedAdRepository.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(cmd.Price, cmd.Currency, _currencyLookup)));

					await _classifiedAdRepository.Save(classifiedAd);
					break;

				case RequestClassifiedAdToPublish_V1 cmd:
					classifiedAd = await _classifiedAdRepository.Load<ClassifiedAd>(cmd.Id.ToString());

					if (classifiedAd == null)
						throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

					classifiedAd.RequestToPublish(Guid.NewGuid());

					await _classifiedAdRepository.Save(classifiedAd);
					break;

				default:
					throw new InvalidOperationException($"Command type {command.GetType().FullName} is unkown");
			}
		}
	}
}
