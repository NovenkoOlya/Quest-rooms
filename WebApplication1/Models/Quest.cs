using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Quest
    {
        [Key]
        public int Quest_ID { get; set; }
        public int Room_ID { get; set; }
        public string Q_Name { get; set; }
        public string Q_Description { get; set; }
        public int Difficulty_Level { get; set; } // 1–5
        public int Duration { get; set; } // хвилини
        public int Min_Players { get; set; }
        public int Max_Players { get; set; }
        public decimal Base_Price { get; set; }

        public Room Room { get; set; }
        public ICollection<Session> Session { get; set; }
    }

}
