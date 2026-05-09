using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OwnerController(ApplicationDbContext context) => _context = context;

        public IActionResult Dashboard()
        {
            var ownerId = int.Parse(User.FindFirst("UserId").Value);
            var rooms = _context.Room.Where(r => r.Owner_ID == ownerId).ToList();
            return View(rooms);
        }
    }

}
