using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Booking
    {
        [Key]
        public int Booking_ID { get; set; }
        public int Client_ID { get; set; }
        public int Session_ID { get; set; }
        public int Players_Count { get; set; }
        public string Client_Contacts { get; set; }
        public string B_Status { get; set; } = "Pending";
        public DateTime B_Creation_Date { get; set; } = DateTime.Now;

        public User Client { get; set; }
        public Session Session { get; set; }
    }

}
