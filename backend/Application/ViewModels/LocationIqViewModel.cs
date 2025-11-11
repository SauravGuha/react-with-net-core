
using Domain.Models;

namespace Application.ViewModels
{
    public class LocationIqViewModel
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
    }
}