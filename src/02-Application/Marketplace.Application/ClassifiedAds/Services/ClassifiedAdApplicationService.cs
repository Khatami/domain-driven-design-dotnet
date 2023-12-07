//using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
//using Marketplace.Application.Contracts.ClassifiedAds.IServices;
//using Marketplace.Application.Infrastructure;
//using Marketplace.Domain.ClassifiedAds;
//using Marketplace.Domain.ClassifiedAds.Arguments;
//using Marketplace.Domain.ClassifiedAds.DomainServices;
//using Marketplace.Domain.ClassifiedAds.ValueObjects;
//using Marketplace.Domain.Shared.ValueObjects;

//namespace Marketplace.Application.ClassifiedAds.Services
//{
//	/// <summary>
//	/// It's completely against SRP
//	/// </summary>
//	internal class ClassifiedAdApplicationService : IClassifiedAdApplicationService
//	{
//		private readonly IClassifiedAdRepository _classifiedAdRepository;
//		private readonly ICurrencyLookup _currencyLookup;
//		private readonly IUnitOfWork _unitOfWork;

//		public ClassifiedAdApplicationService(IClassifiedAdRepository classifiedAdRepository,
//			ICurrencyLookup currencyLookup,
//			IUnitOfWork unitOfWork)
//		{
//			_classifiedAdRepository = classifiedAdRepository;
//			_currencyLookup = currencyLookup;
//			_unitOfWork = unitOfWork;
//		}

//		public async Task Handle(object command)
//		{
//			ClassifiedAd classifiedAd;

//			// Advanced pattern-matching
//			switch (command)
//			{
//				case CreateClassifiedAdCommand cmd:
//				var exists = await _classifiedAdRepository.Exists(new ClassifiedAdId(cmd.Id));

//				if (exists)
//					throw new InvalidOperationException($"Entitywith id {cmd.Id} already exists");

//				classifiedAd = new ClassifiedAd(new ClassifiedAdId(cmd.Id), new UserId(cmd.OwnerId));

//				await _classifiedAdRepository.Add(classifiedAd);

//				await _unitOfWork.Commit();
//				break;

//				case SetClassifiedAdTitleCommand cmd:
//				classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

//				if (classifiedAd == null)
//					throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

//				classifiedAd.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));

//				await _unitOfWork.Commit();
//				break;

//				case UpdateClassifiedAdTextCommand cmd:
//				classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

//				if (classifiedAd == null)
//					throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

//				classifiedAd.UpdateText(ClassifiedAdText.FromString(cmd.Text));

//				await _unitOfWork.Commit();
//				break;

//				case UpdateClassifiedAdPriceCommand cmd:
//				classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

//				if (classifiedAd == null)
//					throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

//				classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(cmd.Price, cmd.Currency, _currencyLookup)));

//				await _unitOfWork.Commit();
//				break;

//				case RequestClassifiedAdToPublishCommand cmd:
//				classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

//				if (classifiedAd == null)
//					throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

//				classifiedAd.RequestToPublish(Guid.NewGuid());

//				await _unitOfWork.Commit();
//				break;

//				default:
//				throw new InvalidOperationException($"Command type {command.GetType().FullName} is unkown");
//			}
//		}
//	}
//}
