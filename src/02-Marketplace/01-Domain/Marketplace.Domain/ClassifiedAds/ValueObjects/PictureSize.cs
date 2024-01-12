namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record PictureSize
	{
		private PictureSize() { }

		public PictureSize(int width, int height)
		{
			if (width < 0)
				throw new ArgumentOutOfRangeException(nameof(width), "Picture width must be a positive number");

			if (height < 0)
				throw new ArgumentOutOfRangeException(nameof(height), "Picture height must be a positive number");

			this.Width = width;
			this.Height = height;
		}

		public int Width { get; private set; }

		public int Height { get; private set; }
	}
}
