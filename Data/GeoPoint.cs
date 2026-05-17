namespace DcsBriefop.Data
{
	internal readonly struct GeoPoint(double dLat, double dLng)
	{
		public double Latitude { get; } = dLat;
		public double Longitude { get; } = dLng;
	}
}
