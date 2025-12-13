using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RHManager.Models;
using RHManager.Services;

namespace RHManager.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string search, bool? isActive)
        {
            var employees = await _employeeService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchLower = search.ToLower();

                employees = employees.Where(e =>
                    ($"{e.FirstName} {e.LastName}").ToLower().Contains(searchLower) ||
                    e.Email.ToLower().Contains(searchLower)
                ).ToList();
            }

            if (isActive.HasValue)
            {
                employees = employees
                    .Where(e => e.IsActive == isActive.Value)
                    .ToList();
            }

            ViewData["Search"] = search;
            ViewData["IsActive"] = isActive;

            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _employeeService.GetByIdAsync(id.Value);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var positions = _context.Positions
                .Include(p => p.Department)
                .Select(p => new
                {
                    p.PositionId,
                    Name = p.Name + " - " + p.Department!.Name
                })
                .ToList();

            ViewData["PositionId"] = new SelectList(positions, "PositionId", "Name");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                var positions = _context.Positions
                    .Include(p => p.Department)
                    .Select(p => new
                    {
                        p.PositionId,
                        Name = p.Name + " - " + p.Department!.Name
                    })
                    .ToList();

                ViewData["PositionId"] = new SelectList(
                    positions,
                    "PositionId",
                    "Name",
                    employee.PositionId
                );

                return View(employee);
            }

            await _employeeService.CreateAsync(employee);
            TempData["Success"] = "Empleado creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null)
                return NotFound();

            var positions = _context.Positions
                .Include(p => p.Department)
                .Select(p => new
                {
                    p.PositionId,
                    Name = p.Name + " - " + p.Department!.Name
                })
                .ToList();

            ViewData["PositionId"] = new SelectList(
                positions,
                "PositionId",
                "Name",
                employee.PositionId
            );

            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var positions = _context.Positions
                    .Include(p => p.Department)
                    .Select(p => new
                    {
                        p.PositionId,
                        Name = p.Name + " - " + p.Department!.Name
                    })
                    .ToList();

                ViewData["PositionId"] = new SelectList(
                    positions,
                    "PositionId",
                    "Name",
                    employee.PositionId
                );

                return View(employee);
            }

            try
            {
                await _employeeService.UpdateAsync(employee);
                TempData["Success"] = "Empleado actualizado correctamente.";
            }
            catch
            {
                TempData["Error"] = "Hubo un error al actualizar el empleado.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteAsync(id);
            TempData["Success"] = "Empleado eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
