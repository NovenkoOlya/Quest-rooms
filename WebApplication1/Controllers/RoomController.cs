using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Owner,Admin,Client")]
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RoomController(ApplicationDbContext context) => _context = context;

        public IActionResult Details(int id)
        {
            var room = _context.Room
                .Include(r => r.Quests)
                .Include(r => r.Review)
                .FirstOrDefault(r => r.Room_ID == id);
            return View(room);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Create() => View();

        [HttpPost, Authorize(Roles = "Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            if (!int.TryParse(User.FindFirst("UserId")?.Value, out var ownerId))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(room);
            }

            room.Owner_ID = ownerId;
            _context.Room.Add(room);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }

}
