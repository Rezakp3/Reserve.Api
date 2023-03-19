using Core.Enums;
using Core.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Locations" , Schema ="dbo")]
    public class Locations : BaseWithDate
    {
        public LocationType LocationType { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Adres { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
