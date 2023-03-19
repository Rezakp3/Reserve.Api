using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Extentions;

namespace Core.Entities
{
    [Table("User", Schema = "dbo")]
    public class User : BaseWithDate
    {
        [MaxLength(15)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(20)]
        public string FName { get; set; }
        [MaxLength(30)]
        public string LName { get; set; }
    }
}
