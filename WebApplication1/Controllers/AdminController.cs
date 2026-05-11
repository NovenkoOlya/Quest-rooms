using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) => _context = context;

        public IActionResult Dashboard()
        {
            var rooms = _context.Room.ToList();
            var booking = _context.Booking
                .Include(b => b.User)
                .Include(b => b.Session)
                .ThenInclude(s => s.Quest)
                .ToList();
            var users = _context.User.OrderBy(u => u.Role).ThenBy(u => u.Full_Name).ToList();
            var reviews = _context.Review
                .Include(r => r.User)
                .Include(r => r.Room)
                .OrderByDescending(r => r.R_Creation_Date)
                .ToList();

            return View(new AdminDashboardViewModel { Rooms = rooms, Booking = booking, Users = users, Reviews = reviews });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveRoom(int id)
        {
            var room = _context.Room.Find(id);
            if (room != null) room.Availability_Status = true;
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectRoom(int id)
        {
            var room = _context.Room.Find(id);
            if (room != null) room.Availability_Status = false;
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelBooking(int id)
        {
            var booking = _context.Booking.Find(id);
            if (booking != null) booking.B_Status = "Cancelled";
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Review.Find(id);
            if (review != null)
            {
                _context.Review.Remove(review);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
    }

}
