using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecordManagement.Web.Domain;

namespace StudentRecordManagement.Web.Pages;

public class TranscriptModel : PageModel
{
    private readonly Registrar _registrar;

    public TranscriptModel(Registrar registrar) => _registrar = registrar;

    [BindProperty(SupportsGet = true)]
    public string? StudentId { get; set; }

    public Student? Found { get; private set; }
    public bool Searched { get; private set; }

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(StudentId))
        {
            return;
        }

        Searched = true;
        Found = _registrar.FindStudent(StudentId.Trim());
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
