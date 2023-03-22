﻿using Marketplace.Domain.ClassifiedAd.Arguments;
using Marketplace.Domain.ClassifiedAd.DomainServices;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record Price : Money
	{
		public Price(MoneyArguments moneyArguments) : base(moneyArguments)
		{
			if (moneyArguments.Amount < 0)
				throw new ArgumentException("Price cannot be negative", nameof(moneyArguments.Amount));
		}
	}
}
