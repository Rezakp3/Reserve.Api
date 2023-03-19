using Core.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Auth" , Schema = "dbo")]
    public class Auth : BaseEntity
    {
        [MaxLength(300)]
        public string AccessToken{ get; set; }

        [MaxLength(300)]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
