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
            var rooms = _context.QuestRoom.ToList();
            var booking = _context.Booking
                .Include(b => b.Session)
                .ThenInclude(s => s.Quest)
                .ToList();
            return View(new AdminDashboardViewModel { Rooms = rooms, Booking = booking });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveRoom(int id)
        {
            var room = _context.QuestRoom.Find(id);
            if (room != null) room.Availability_Status = true;
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectRoom(int id)
        {
            var room = _context.QuestRoom.Find(id);
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
    }

}
