using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecordManagement.Web.Domain;

namespace StudentRecordManagement.Web.Pages;

public class IndexModel : PageModel
{
    private readonly Registrar _registrar;

    public IndexModel(Registrar registrar) => _registrar = registrar;

    public int TotalUnits { get; private set; }
    public int TotalCourses { get; private set; }
    public int TotalStudents { get; private set; }
    public double AverageWam { get; private set; }
    public List<Student> TopStudents { get; private set; } = new();
    public List<(string Course, int UnitCount)> CourseBreakdown { get; private set; } = new();

    public void OnGet()
    {
        TotalUnits = _registrar.Units.Count;
        TotalCourses = _registrar.Units.Select(u => u.Course).Distinct().Count();
        TotalStudents = _registrar.Students.Count;
        AverageWam = _registrar.Students.Count == 0
            ? 0
            : _registrar.Students.Average(s => s.Wam);

        TopStudents = _registrar.Students
            .OrderByDescending(s => s.Wam)
            .Take(5)
            .ToList();

        CourseBreakdown = _registrar.Units
            .GroupBy(u => u.Course)
            .Select(g => (g.Key, g.Count()))
            .OrderByDescending(g => g.Item2)
            .ToList();
    }
}
