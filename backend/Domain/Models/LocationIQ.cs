

namespace Domain.Models
{
    public class LocationIQ
    {
        public string? Place_Id { get; set; }
        public string? Osm_Id { get; set; }
        public string? Osm_Type { get; set; }
        public string? Licence { get; set; }
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        public List<string>? Boundingbox { get; set; }
        public string? Display_Name { get; set; }
        public string? Display_Place { get; set; }
        public string? Display_Address { get; set; }
        public Address? Address { get; set; }
    }

    public class Address
    {
        public string? Name { get; set; }
        public string? Suburb { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Country_Code { get; set; }
    }
}