namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record PictureId
	{
		public PictureId(Guid value)
		{
			Value = value;
		}

		public Guid Value { get; }
	}
}