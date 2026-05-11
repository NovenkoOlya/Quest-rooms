using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            var user = _context.User
                .FirstOrDefault(u => (u.Email == login || u.Phone_Number == login)
                                  && u.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, user.User_ID.ToString()),
                    new Claim(ClaimTypes.Name, user.Full_Name ?? user.Email ?? user.Phone_Number),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Quest"); // головна сторінка
            }

            ModelState.AddModelError("", "Невірний логін або пароль");
            return View();
        }

        // GET: Register
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string Full_Name, string Email, string Phone_Number, string Password, string Role)
        {
            var user = new User
            {
                Full_Name = Full_Name,
                Email = Email,
                Phone_Number = Phone_Number,
                Password = Password,
                Role = Role,
                Registration_Date = DateTime.Now,
                Account_Status = "Active"
            };

            _context.User.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Quest");
        }
    }

}
