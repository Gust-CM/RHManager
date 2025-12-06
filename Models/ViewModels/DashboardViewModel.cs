using System.Collections.Generic;

namespace RHManager.Models.ViewModels
{
    public class DashboardViewModel
    {
        // KPIs principales
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int PendingLeaves { get; set; }
        public int TotalPositions { get; set; }

        // KPIs avanzados
        public double AvgLeaveDays { get; set; }
        public double MonthlyGrowthRate { get; set; }
        public double TurnoverRate { get; set; }

        // Charts
        public List<string> ChartLabels { get; set; } = new();
        public List<int> ChartValues { get; set; } = new();

        public List<string> DeptLabels { get; set; } = new();
        public List<int> DeptEmployees { get; set; } = new();

        public List<string> PositionLabels { get; set; } = new();
        public List<int> PositionEmployees { get; set; } = new();

        // Tablas inteligentes
        public List<Employee> LatestEmployees { get; set; } = new();
        public List<LeaveRequest> RecentLeaveRequests { get; set; } = new();
    }
}
