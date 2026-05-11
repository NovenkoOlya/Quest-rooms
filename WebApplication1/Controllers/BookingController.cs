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

        [Authorize(Roles = "Client")]
        public IActionResult Create(int questId)
        {
            var sessions = _context.Session
                .Include(s => s.Quest)
                .Where(s => s.Quest_ID == questId && s.S_Status == "Available")
                .OrderBy(s => s.Start_DateTime)
                .ToList();

            ViewBag.Sessions = sessions
                .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Session_ID.ToString(),
                    Text = $"{s.Start_DateTime:dd.MM.yyyy HH:mm} - {s.S_Price} грн"
                })
                .ToList();

            return View(new Booking());
        }

        [HttpPost, Authorize(Roles = "Client")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int sessionId, int playersCount, string? clientContacts)
        {
            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out var clientId))
            {
                return Unauthorized();
            }

            if (playersCount < 1)
            {
                return BadRequest("Players count must be greater than zero.");
            }

            var booking = new Booking
            {
                User_ID = clientId,
                Session_ID = sessionId,
                Players_Count = playersCount,
                Client_Contacts = clientContacts,
                B_Status = "Pending"
            };
            _context.Booking.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("MyBooking");
        }

        [Authorize(Roles = "Client")]
        public IActionResult MyBooking()
        {
            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out var clientId))
            {
                return Unauthorized();
            }

            var Booking = _context.Booking
                .Include(b => b.Session)
                .ThenInclude(s => s.Quest)
                .Where(b => b.User_ID == clientId).ToList();
            return View("MyBookings", Booking);
        }

        [HttpPost, Authorize(Roles = "Client")]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var booking = _context.Booking.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            if (!int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out var clientId))
            {
                return Unauthorized();
            }

            if (booking.User_ID != clientId)
            {
                return Forbid();
            }

            if (booking != null) booking.B_Status = "Cancelled";
            _context.SaveChanges();
            return RedirectToAction("MyBooking");
        }
    }

}
