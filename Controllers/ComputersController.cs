using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ComputersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComputersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Computers.ToListAsync());
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComputerId,Name,Status,PricePerHour")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(computer);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComputerId,Name,Status,PricePerHour")] Computer computer)
        {
            if (id != computer.ComputerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.ComputerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers
                .FirstOrDefaultAsync(m => m.ComputerId == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer != null)
            {
                _context.Computers.Remove(computer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(int id)
        {
            return _context.Computers.Any(e => e.ComputerId == id);
        }
        public async Task<IActionResult> Choose()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var user = await _context.Users.FindAsync(userId.Value);
                ViewBag.Balance = user?.Balance ?? 0;
            }

            var availableComputers = await _context.Computers
                .Where(c => !c.IsInUse)
                .OrderBy(c => c.Name)       // <-- Sắp xếp theo Name
                .ToListAsync();

            var model = new ChooseModel
            {
                Computers = availableComputers
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StartUsing(int computerId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var computer = await _context.Computers.FindAsync(computerId);
            if (computer == null || computer.IsInUse)
                return NotFound();

            // Đánh dấu là đang sử dụng
            computer.IsInUse = true;

            // Tạo bản ghi UsageHistory mới
            var usage = new UsageHistory
            {
                ComputerId = computerId,
                UserId = userId.Value,
                StartTime = DateTime.Now
            };

            _context.UsageHistories.Add(usage);
            await _context.SaveChangesAsync();

            // Tùy ý: Chuyển sang trang hiển thị thông tin máy đang dùng
            return RedirectToAction("UsingNow", new { id = usage.UsageHistoryId });
        }
        public async Task<IActionResult> UsingNow(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var user = await _context.Users.FindAsync(userId.Value);
                ViewBag.Balance = user?.Balance ?? 0;
            }
            var usage = await _context.UsageHistories
                .Include(u => u.Computer)
                .Include(u => u.User)
                .FirstOrDefaultAsync(u => u.UsageHistoryId == id);

            if (usage == null)
            {
                return NotFound();
            }

            return View(usage);
        }
        [HttpPost]
        public async Task<IActionResult> EndUsing(int usageHistoryId)
        {
            var usage = await _context.UsageHistories
                .Include(u => u.Computer)
                .FirstOrDefaultAsync(u => u.UsageHistoryId == usageHistoryId);

            if (usage == null || usage.EndTime != null)
                return NotFound();

            usage.EndTime = DateTime.Now;

            // Tính thời gian sử dụng (làm tròn lên 5 phút gần nhất nếu muốn)
            var duration = usage.EndTime.Value - usage.StartTime;
            var hours = (decimal)duration.TotalMinutes / 60;
            usage.TotalCost = Math.Round(hours * usage.Computer.PricePerHour, 2);

            // Trừ tiền tài khoản người dùng
            var user = await _context.Users.FindAsync(usage.UserId);
            if (user != null)
            {
                // Check đủ tiền (optional nếu cậu muốn ngăn không đủ tiền)
                if (user.Balance >= usage.TotalCost)
                {
                    user.Balance -= usage.TotalCost;
                }
                else
                {
                    // Không đủ tiền (nếu cần xử lý riêng)
                    ViewBag.Message = "Không đủ tiền để thanh toán.";
                    return View("PaymentFailed", usage); // cậu có thể tạo view này nếu muốn
                }
            }

            // Đánh dấu máy không còn đang sử dụng
            usage.Computer.IsInUse = false;

            await _context.SaveChangesAsync();

            // Trả về view hiển thị tóm tắt (Summary.cshtml)
            return View("Summary", usage);
        }

    }
}
