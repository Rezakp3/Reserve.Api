using Core.Enums;

namespace Core.Dtos
{
    public class LocationSearchDto
    {
        public string Title { get; set; }
        public int LocationType { get; set; }
        public int pageNumber { get; set; }
        public int itemCount { get; set; }
    }
}
