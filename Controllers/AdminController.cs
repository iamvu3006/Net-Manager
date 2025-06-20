using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> RevenueByComputer()
    {
        var revenueData = await _context.UsageHistories
            .Where(u => u.EndTime != null)
            .GroupBy(u => u.Computer.Name)
            .Select(g => new
            {
                ComputerName = g.Key,
                TotalRevenue = g.Sum(x => x.TotalCost),
                TotalHours = g.Sum(x => EF.Functions.DateDiffMinute(x.StartTime, x.EndTime.Value)) / 60.0
            })
            .ToListAsync();

        ViewBag.RevenueData = revenueData;
        return View();
    }

    public async Task<IActionResult> RevenueByDate()
    {
        var revenueData = await _context.UsageHistories
            .Where(u => u.EndTime != null)
            .GroupBy(u => u.EndTime.Value.Date)
            .Select(g => new
            {
                Date = g.Key,
                TotalRevenue = g.Sum(x => x.TotalCost)
            })
            .ToListAsync();

        ViewBag.RevenueData = revenueData;
        return View();
    }
}
