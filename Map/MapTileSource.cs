using BruTile;

namespace DcsBriefop.Map
{
	internal class MapTileSource(string sName, Func<ITileSource> tileFactory)
	{
		public string Name { get; private set; } = sName;
		public virtual bool Active { get; set; } = true;
		public Func<ITileSource> TileFactory { get; set; } = tileFactory;
	}
}
