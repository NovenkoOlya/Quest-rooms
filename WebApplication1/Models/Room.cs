using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Room
    {
        [Key]
        public int Room_ID { get; set; }
        [ForeignKey("Owner")] // Вказуємо на властивість-об'єкт
        public int Owner_ID { get; set; }
        public string QR_Name { get; set; }
        public string QR_Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Contact_Info { get; set; }
        public bool Availability_Status { get; set; } = true;
        public DateTime QR_Creation_Date { get; set; } = DateTime.Now;

        public User Owner { get; set; }
        public ICollection<Quest> Quests { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
