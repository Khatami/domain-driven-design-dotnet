namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record PictureSize
	{
		public PictureSize(int width, int height)
		{
			if (width < 0)
				throw new ArgumentOutOfRangeException(nameof(width), "Picture width must be a positive number");

			if (height < 0)
				throw new ArgumentOutOfRangeException(nameof(height), "Picture height must be a positive number");

			this.Width = width;
			this.Height = height;
		}

		public int Width { get; }

		public int Height { get; }
	}
}
