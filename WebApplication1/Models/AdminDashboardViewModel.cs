namespace WebApplication1.Models
{
    public class AdminDashboardViewModel
    {
        public List<Room> Rooms { get; set; }
        public List<Booking> Booking { get; set; }
        public List<User> Users { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
