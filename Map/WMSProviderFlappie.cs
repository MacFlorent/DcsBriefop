namespace DcsBriefop.Map
{
	internal class WMSProviderFlappie : WMSProviderBase
	{
		public WMSProviderFlappie()
		{
			Name = "Flappie Caucasus";
			Url = "http://dcsmaps.com/cgi-bin/mapserv?map=CAUCASUS_MAPFILE";
			WmsLayer = "LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR";
		}
	}
}
