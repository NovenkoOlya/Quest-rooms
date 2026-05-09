using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Client")]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context) => _context = context;

        public IActionResult Create(int roomId) => View(new Review { Room_ID = roomId });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (!int.TryParse(User.FindFirst("UserId")?.Value, out var clientId))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(review);
            }

            review.Client_ID = clientId;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("Details", "Room", new { id = review.Room_ID });
        }
    }

}
