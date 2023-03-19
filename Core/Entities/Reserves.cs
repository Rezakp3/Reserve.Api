using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extentions;

namespace Core.Entities
{
    [Table("Reserves", Schema = "dbo")]
    public class Reserves : BaseWithDate
    {
        public DateTime ReserveDate { get; set; }
        public Guid ReserverId { get; set; }
        public Guid LocationId { get; set; }
        public int Price { get; set; }
    }
}
