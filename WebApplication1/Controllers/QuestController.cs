using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class QuestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public QuestController(ApplicationDbContext context) => _context = context;

        public IActionResult Details(int id)
        {
            var quest = _context.Quests
                .Include(q => q.Session)
                .FirstOrDefault(q => q.Quest_ID == id);
            return View(quest);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Create(int roomId) => View(new Quest { Room_ID = roomId });

        [HttpPost, Authorize(Roles = "Owner")]
        public IActionResult Create(Quest quest)
        {
            _context.Quests.Add(quest);
            _context.SaveChanges();
            return RedirectToAction("Details", "Room", new { id = quest.Room_ID });
        }
    }
}
