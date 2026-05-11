using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_Number { get; set; }
        public string Role { get; set; } = "Client";
        public DateTime? Registration_Date { get; set; } = DateTime.Now;
        public string? Account_Status { get; set; } = "Active";
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
