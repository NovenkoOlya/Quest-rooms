using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Session
    {
        [Key]
        public int Session_ID { get; set; }
        public int Quest_ID { get; set; }
        public DateTime? Start_DateTime { get; set; }
        public decimal? S_Price { get; set; }
        public string? S_Status { get; set; } = "Available";

        public Quest? Quest { get; set; }
        public ICollection<Booking> Booking { get; set; } = new List<Booking>();
    }

}
