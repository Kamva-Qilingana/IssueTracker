using IssueTracker.Data;
using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        // Single constructor injecting both logger and db context
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // GET: /Home/Index
        public async Task<IActionResult> Index(string? q)
        {
            var query = _db.Issues.Include(i => i.IssueType).AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(i =>
                    (i.Description ?? "").Contains(q) ||
                    (i.IssueType != null && i.IssueType.IssueName.Contains(q))
                );
            }

            query = query.OrderByDescending(i => i.CreatedAt);

            var model = new IssueListViewModel
            {
                Issues = await query.ToListAsync(),
                NewIssue = new Issue(),
                IssueTypes = await _db.IssueTypes.ToListAsync() // populate IssueTypes for dropdown
            };

            return View(model);
        }
        // POST: /Issues/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssueListViewModel vm)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            _db.Issues.Add(vm.NewIssue);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Issues/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var issue = await _db.Issues.FindAsync(id);
            return issue == null ? NotFound() : View(issue);
        }

        // POST: /Issues/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Issue issue)
        {
            //if (id != issue.IssueId) return BadRequest();
            //if (!ModelState.IsValid) return View(issue);

            var existing = await _db.Issues.FindAsync(id);
            if (existing == null) return NotFound();


            existing.Description = issue.Description;
            existing.Status = issue.Status;
            existing.Priority = issue.Priority;
            existing.IssueTypeId = issue.IssueTypeId;
            existing.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Issues/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _db.Issues.FindAsync(id);
            if (issue != null)
            {
                _db.Issues.Remove(issue);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
