using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManager.Models;
using RHManager.Models.ViewModels;

namespace RHManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel();

            // --------------------------------------------------------------------
            // 1. KPIs PRINCIPALES
            // --------------------------------------------------------------------
            vm.TotalEmployees = await _context.Employees.CountAsync();
            vm.ActiveEmployees = await _context.Employees.CountAsync(e => e.IsActive);
            vm.PendingLeaves = await _context.LeaveRequests.CountAsync(l => l.Status == 0); // Pendientes
            vm.TotalPositions = await _context.Positions.CountAsync();


            // --------------------------------------------------------------------
            // 2. KPI AVANZADO → Promedio de días de permiso (SOLO aprobados)
            // --------------------------------------------------------------------
            var approvedLeaves = await _context.LeaveRequests
                .Where(l => l.Status == 1)
                .ToListAsync(); // ← Necesario para evitar fallas de EF Core

            vm.AvgLeaveDays = approvedLeaves.Any()
                ? Math.Round(approvedLeaves.Average(l => (l.EndDate - l.StartDate).TotalDays), 2)
                : 0;


            // --------------------------------------------------------------------
            // 3. CRECIMIENTO MENSUAL DE CONTRATACIONES
            // --------------------------------------------------------------------
            int thisMonth = DateTime.Now.Month;
            int prevMonth = thisMonth == 1 ? 12 : thisMonth - 1;

            int hiresThisMonth = await _context.Employees
                .CountAsync(e => e.HireDate.Month == thisMonth);

            int hiresPrevMonth = await _context.Employees
                .CountAsync(e => e.HireDate.Month == prevMonth);

            vm.MonthlyGrowthRate = hiresPrevMonth == 0
                ? (hiresThisMonth > 0 ? 100 : 0)
                : Math.Round(((double)(hiresThisMonth - hiresPrevMonth) / hiresPrevMonth) * 100, 2);


            // --------------------------------------------------------------------
            // 4. TASA DE ROTACIÓN (empleados inactivos / totales)
            // --------------------------------------------------------------------
            int inactiveEmployees = await _context.Employees.CountAsync(e => !e.IsActive);

            vm.TurnoverRate = vm.TotalEmployees == 0
                ? 0
                : Math.Round((double)inactiveEmployees / vm.TotalEmployees * 100, 2);


            // --------------------------------------------------------------------
            // 5. Últimos movimientos: Permisos
            // --------------------------------------------------------------------
            vm.RecentLeaveRequests = await _context.LeaveRequests
                .Include(l => l.Employee)
                .OrderByDescending(l => l.LeaveRequestId)
                .Take(5)
                .ToListAsync();


            // --------------------------------------------------------------------
            // 6. Últimos empleados ingresados
            // --------------------------------------------------------------------
            vm.LatestEmployees = await _context.Employees
                .OrderByDescending(e => e.EmployeeId)
                .Take(5)
                .ToListAsync();


            // --------------------------------------------------------------------
            // 7. Gráfica: Permisos por mes (enero – diciembre)
            // --------------------------------------------------------------------
            vm.ChartLabels = Enumerable.Range(1, 12)
                .Select(m => new DateTime(2025, m, 1).ToString("MMM"))
                .ToList();

            vm.ChartValues = Enumerable.Range(1, 12)
                .Select(m => _context.LeaveRequests.Count(l => l.StartDate.Month == m))
                .ToList();


            // --------------------------------------------------------------------
            // 8. Distribución por departamento
            //    (departamento → cantidad de empleados)
            // --------------------------------------------------------------------
            vm.DeptLabels = await _context.Departments
                .OrderBy(d => d.Name)
                .Select(d => d.Name)
                .ToListAsync();

            vm.DeptEmployees = await _context.Departments
                .OrderBy(d => d.Name)
                .Select(d => d.Positions.SelectMany(p => p.Employees).Count())
                .ToListAsync();


            // --------------------------------------------------------------------
            // 9. Distribución por Puesto
            // --------------------------------------------------------------------
            vm.PositionLabels = await _context.Positions
                .OrderBy(p => p.Name)
                .Select(p => p.Name)
                .ToListAsync();

            vm.PositionEmployees = await _context.Positions
                .OrderBy(p => p.Name)
                .Select(p => p.Employees.Count)
                .ToListAsync();


            // --------------------------------------------------------------------
            // 10. RETURN → Vista cargada con el ViewModel completo
            // --------------------------------------------------------------------
            return View(vm);
        }
    }
}
