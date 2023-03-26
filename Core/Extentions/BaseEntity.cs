using System.ComponentModel.DataAnnotations;

namespace Core.Extentions
{
    public class BaseEntity 
    {
        [Key]
        public Guid Id { get; set; }
    }
}
