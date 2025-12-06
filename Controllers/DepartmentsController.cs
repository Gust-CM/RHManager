using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManager.Models;

namespace RHManager.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var data = await _context.Departments
                .OrderBy(d => d.Name)
                .ToListAsync();

            return View(data);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create() => View();

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid)
                return View(department);

            _context.Add(department);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Department created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        // POST: Departments/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments.Any(d => d.DepartmentId == id))
                    return NotFound();

                throw;
            }

            TempData["Success"] = "Department updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // POST: Departments/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department != null)
                _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
