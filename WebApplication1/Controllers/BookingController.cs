using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Client,Admin")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookingController(ApplicationDbContext context) => _context = context;

        [HttpPost, Authorize(Roles = "Client")]
        public IActionResult Create(int sessionId, int playersCount)
        {
            var booking = new Booking
            {
                Client_ID = int.Parse(User.FindFirst("UserId").Value),
                Session_ID = sessionId,
                Players_Count = playersCount,
                B_Status = "Pending"
            };
            _context.Booking.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("MyBooking");
        }

        [Authorize(Roles = "Client")]
        public IActionResult MyBooking()
        {
            var clientId = int.Parse(User.FindFirst("UserId").Value);
            var Booking = _context.Booking
                .Include(b => b.Session)
                .Where(b => b.Client_ID == clientId).ToList();
            return View(Booking);
        }

        [HttpPost, Authorize(Roles = "Client")]
        public IActionResult Cancel(int id)
        {
            var booking = _context.Booking.Find(id);
            if (booking != null) booking.B_Status = "Cancelled";
            _context.SaveChanges();
            return RedirectToAction("MyBooking");
        }
    }

}
