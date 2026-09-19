using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecordManagement.Web.Domain;

namespace StudentRecordManagement.Web.Pages;

public class StudentsModel : PageModel
{
    private readonly Registrar _registrar;

    public StudentsModel(Registrar registrar) => _registrar = registrar;

    [BindProperty(SupportsGet = true)]
    public string? Id { get; set; }

    public List<Student> AllStudents { get; private set; } = new();
    public Student? Selected { get; private set; }

    public void OnGet()
    {
        AllStudents = _registrar.Students.OrderBy(s => s.Name).ToList();

        if (!string.IsNullOrWhiteSpace(Id))
        {
            Selected = _registrar.FindStudent(Id);
        }
    }

    public static string GradeCssClass(string grade) => grade switch
    {
        "HD" => "grade-hd",
        "D" => "grade-d",
        "C" => "grade-c",
        "P" => "grade-p",
        "N" => "grade-n",
        _ => "grade-na",
    };
}
