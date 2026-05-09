using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var rooms = _context.QuestRoom.AsNoTracking().ToList();
                return View(rooms);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Database transient failure while loading rooms for home page.");
                TempData["DbError"] = "Тимчасова помилка з'єднання з базою даних. Спробуйте ще раз через кілька секунд.";
                return View(Enumerable.Empty<WebApplication1.Models.QuestRoom>());
            }
        }
    }
}
