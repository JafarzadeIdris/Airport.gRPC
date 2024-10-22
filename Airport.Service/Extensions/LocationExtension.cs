namespace Airport.Service.Extensions
{
    public static class LocationExtensions
    {
        private const double EarthRadiusKm = 6371; // km

        public static double CalculateDistance(this (double Latitude, double Longitude) point1, (double Latitude, double Longitude) point2)
        {
            var dLat = DegreesToRadians(point2.Latitude - point1.Latitude);
            var dLon = DegreesToRadians(point2.Longitude - point1.Longitude);

            var lat1 = DegreesToRadians(point1.Latitude);
            var lat2 = DegreesToRadians(point2.Latitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
