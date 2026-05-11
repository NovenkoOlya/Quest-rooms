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
        public IActionResult AddReview(int Room_ID, int Rating, string Review_Text)
        {
            var userClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userClaim == null)
            {
                return RedirectToAction("Login", "Authorization");
            }
            var userId = int.Parse(userClaim.Value);


            var review = new Review
            {
                Room_ID = Room_ID,
                User_ID = userId,
                Rating = Rating,
                Review_Text = Review_Text,
                R_Creation_Date = DateTime.Now
            };

            _context.Review.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "QuestRoom", new { id = Room_ID });
        }


    }
}
