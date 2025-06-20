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
    public class UsageHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsageHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsageHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UsageHistories.Include(u => u.Computer).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
