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
        public IActionResult Create(Review review)
        {
            review.Client_ID = int.Parse(User.FindFirst("UserId").Value);
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("Details", "Room", new { id = review.Room_ID });
        }
    }

}
