using IssueTracker.Data;
using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace IssueTracker.Controllers
{
    public class IssueTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IssueTypes
        public IActionResult Index()
        {
            var issueTypes = _context.IssueTypes.ToList();
            return View(issueTypes);
        }

        // GET: IssueTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IssueType issueType)
        {
            
                _context.IssueTypes.Add(issueType);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
           // return View(issueType);
        }

        // POST: IssueTypes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                _context.IssueTypes.Update(issueType);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index)); // fallback
        }

        // POST: IssueTypes/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int issueTypeId)
        {
            var issueType = _context.IssueTypes.Find(issueTypeId);
            if (issueType != null)
            {
                _context.IssueTypes.Remove(issueType);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
