using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Review
    {
        [Key]
        public int Review_ID { get; set; }

        [ForeignKey(nameof(Client))]
        public int User_ID { get; set; }

        [ForeignKey("Room")]
        public int Room_ID { get; set; }

        public int? Rating { get; set; } // 1–5
        public string? Review_Text { get; set; }
        public DateTime? R_Creation_Date { get; set; } = DateTime.Now;

        public User? Client { get; set; }
        public Room? Room { get; set; }
    }

}
