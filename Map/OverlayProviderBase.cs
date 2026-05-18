using BruTile;

namespace DcsBriefop.Map
{
	internal abstract class OverlayProviderBase
	{
		public string Name { get; set; }
		public abstract ITileSource CreateTileSource();
	}
}
