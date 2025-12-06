using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RHManager.Models;

namespace RHManager.Controllers
{
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var list = await _context.LeaveRequests
                .Include(l => l.Employee)
                .ToListAsync();

            return View(list);
        }

        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            LoadEmployeesDropdown();
            return View();
        }

        // POST: LeaveRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequest request)
        {
            if (!ModelState.IsValid)
            {
                LoadEmployeesDropdown(request.EmployeeId);
                return View(request);
            }

            _context.LeaveRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Solicitud creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var req = await _context.LeaveRequests.FindAsync(id);
            if (req == null)
                return NotFound();

            LoadEmployeesDropdown(req.EmployeeId);
            return View(req);
        }

        // POST: LeaveRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveRequest request)
        {
            if (id != request.LeaveRequestId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                LoadEmployeesDropdown(request.EmployeeId);
                return View(request);
            }

            _context.Update(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Solicitud actualizada.";
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var req = await _context.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(l => l.LeaveRequestId == id);

            if (req == null)
                return NotFound();

            return View(req);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var req = await _context.LeaveRequests.FindAsync(id);
            if (req != null)
            {
                _context.LeaveRequests.Remove(req);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Solicitud eliminada.";
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // Helper privado
        // -----------------------------
        private void LoadEmployeesDropdown(int? selectedEmployeeId = null)
        {
            var employees = _context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    FullName = e.FirstName + " " + e.LastName
                })
                .OrderBy(e => e.FullName)
                .ToList();

            ViewBag.EmployeeId = new SelectList(employees, "EmployeeId", "FullName", selectedEmployeeId);
        }

    }
}
