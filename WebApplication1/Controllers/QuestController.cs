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

        // GET: Quest/Index
        public IActionResult Index(string genreFilter, string difficultyFilter,
                                   decimal? minPrice, decimal? maxPrice,
                                   string sortOrder)
        {
            var quests = _context.Quest.AsQueryable();

            // Фільтр за складністю
            if (!string.IsNullOrEmpty(difficultyFilter) && difficultyFilter != "all")
            {
                if (int.TryParse(difficultyFilter, out int diff))
                    quests = quests.Where(q => q.Difficulty_Level == diff);
            }

            // Фільтр за ціною
            if (minPrice.HasValue)
                quests = quests.Where(q => q.Base_Price >= minPrice.Value);

            if (maxPrice.HasValue)
                quests = quests.Where(q => q.Base_Price <= maxPrice.Value);

            // Сортування
            switch (sortOrder)
            {
                case "price_desc":
                    quests = quests.OrderByDescending(q => q.Base_Price);
                    break;
                case "price_asc":
                    quests = quests.OrderBy(q => q.Base_Price);
                    break;
                case "difficulty_desc":
                    quests = quests.OrderByDescending(q => q.Difficulty_Level);
                    break;
                default:
                    quests = quests.OrderBy(q => q.Quest_ID);
                    break;
            }

            // Для ViewBag — списки жанрів і складностей
           
            ViewBag.Difficulties = _context.Quest.Select(q => q.Difficulty_Level.ToString()).Distinct().ToList();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["GenreFilter"] = genreFilter;
            ViewData["DifficultyFilter"] = difficultyFilter;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            return View(quests.ToList());
        }

        // GET: Quest/Details/5
        public IActionResult Details(int id)
        {
            var quest = _context.Quest
                .Include(q => q.Room)
                .ThenInclude(r => r.Review)
                .ThenInclude(rv => rv.User)
                .FirstOrDefault(q => q.Quest_ID == id);

            if (quest == null) return NotFound();

            return View(quest);
        }



        [Authorize(Roles = "Owner")]
        public IActionResult Create(int roomId) => View(new Quest { Room_ID = roomId });

        [HttpPost, Authorize(Roles = "Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Quest quest)
        {
            if (!ModelState.IsValid)
            {
                return View(quest);
            }

            _context.Quest.Add(quest);
            _context.SaveChanges();
            return RedirectToAction("Details", "Room", new { id = quest.Room_ID });
        }
    }
}
