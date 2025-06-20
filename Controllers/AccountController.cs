using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MvcMovie.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ----------- [GET] REGISTER ----------- 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ----------- [POST] REGISTER ----------- 
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra username đã tồn tại chưa
                var exists = await _context.Users.AnyAsync(u => u.Username == user.Username);
                if (exists)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(user);
                }

                // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                user.Password = HashPassword(user.Password);

                // Lưu người dùng mới vào DB
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Điều hướng sang trang đăng nhập
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // ----------- [GET] LOGIN ----------- 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ----------- [POST] LOGIN ----------- 
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            // Kiểm tra nếu user không tồn tại hoặc mật khẩu không khớp
            if (user == null || !VerifyPassword(password, user.Password))
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Lưu thông tin người dùng vào Session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);

            // Chuyển hướng về trang chính hoặc chọn máy tính nếu là user
            if (user.IsAdmin)
            {
                return RedirectToAction("Index", "Computers"); // Trang quản trị cho Admin
            }
            else
            {
                return RedirectToAction("Choose", "Computers"); // Trang chọn máy tính cho người dùng
            }
        }

        // ----------- LOGOUT ----------- 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Hàm mã hóa mật khẩu (SHA256)
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // Hàm kiểm tra mật khẩu
        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return HashPassword(inputPassword) == storedPassword;
        }

        public IActionResult TopUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TopUp(decimal amount)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.Balance += amount;
            _context.Update(user);
            await _context.SaveChangesAsync();

            ViewBag.Message = $"Nạp thành công {amount:N0} VNĐ!";
            ViewBag.Balance = user.Balance;
            return View();
        }
    }
}
