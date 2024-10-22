namespace Airport.Provider.Models
{
    public class AirportModel
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public Location? Location { get; set; }
        public string? Type { get; set; }
    }
}
