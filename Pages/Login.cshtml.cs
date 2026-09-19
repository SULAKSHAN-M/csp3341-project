using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecordManagement.Web.Domain;

namespace StudentRecordManagement.Web.Pages;

public class LoginModel : PageModel
{
    private const string SessionKey = "AdminStaffId";
    private static readonly Random IdRandom = new();

    private readonly Registrar _registrar;

    public LoginModel(Registrar registrar) => _registrar = registrar;

    // ---- Login form ----
    [BindProperty]
    public string StaffId { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    // ---- Which dashboard panel is open (?view=students|units|enrol|grade) ----
    [BindProperty(SupportsGet = true)]
    public string View { get; set; } = "dashboard";

    // ---- Register / view students ----
    [BindProperty]
    public string NewStudentName { get; set; } = string.Empty;

    [BindProperty]
    public string NewStudentCourse { get; set; } = string.Empty;

    // ---- Add / view units ----
    [BindProperty]
    public string NewUnitCode { get; set; } = string.Empty;

    [BindProperty]
    public string NewUnitTitle { get; set; } = string.Empty;

    [BindProperty]
    public string NewUnitCourse { get; set; } = string.Empty;

    [BindProperty]
    public int NewUnitCreditPoints { get; set; } = 15;

    // ---- Enrol a student ----
    [BindProperty]
    public string EnrolStudentId { get; set; } = string.Empty;

    [BindProperty]
    public string EnrolUnitCode { get; set; } = string.Empty;

    [BindProperty]
    public string EnrolSemester { get; set; } = "2026-2";

    // ---- Record a grade ----
    [BindProperty]
    public string GradeStudentId { get; set; } = string.Empty;

    [BindProperty]
    public string GradeUnitCode { get; set; } = string.Empty;

    [BindProperty]
    public int GradeMark { get; set; }

    public Administrator? LoggedInAdmin { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? StatusMessage { get; private set; }

    // ---- Data for the dashboard/panels ----
    public int TotalStudents { get; private set; }
    public int TotalUnits { get; private set; }
    public int TotalEnrolments { get; private set; }
    public List<Student> AllStudents { get; private set; } = new();
    public List<Unit> AllUnits { get; private set; } = new();
    public List<string> CourseNames { get; private set; } = new();

    public void OnGet()
    {
        LoadLoggedInAdmin();
        LoadDashboardData();
    }

    public IActionResult OnPostLogin()
    {
        var admin = _registrar.FindAdministrator(StaffId);

        // DEMO ONLY: plaintext comparison. A real system must never do this —
        // see the comment on Administrator.Password for what to use instead.
        if (admin is null || admin.Password != Password)
        {
            ErrorMessage = "Invalid staff ID or password.";
            Password = string.Empty;   // don't redisplay the attempted password
            LoadDashboardData();
            return Page();
        }

        HttpContext.Session.SetString(SessionKey, admin.StaffId);
        return RedirectToPage();
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove(SessionKey);
        return RedirectToPage();
    }

    public IActionResult OnPostRegisterStudent()
    {
        if (!RequireLogin(out var redirect)) return redirect!;

        if (string.IsNullOrWhiteSpace(NewStudentName) || string.IsNullOrWhiteSpace(NewStudentCourse))
        {
            ErrorMessage = "Please provide both a name and a course.";
            LoadDashboardData();
            View = "students";
            return Page();
        }

        var newId = GenerateUniqueStudentId();
        _registrar.Students.Add(new Student
        {
            PersonId = newId,
            Name = NewStudentName.Trim(),
            Course = NewStudentCourse.Trim(),
        });

        StatusMessage = $"Registered {NewStudentName} with student ID {newId}.";
        return RedirectToPage(new { view = "students" });
    }

    public IActionResult OnPostAddUnit()
    {
        if (!RequireLogin(out var redirect)) return redirect!;

        var code = NewUnitCode.Trim().ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(NewUnitTitle) || string.IsNullOrWhiteSpace(NewUnitCourse))
        {
            ErrorMessage = "Please provide a unit code, title, and course.";
            LoadDashboardData();
            View = "units";
            return Page();
        }

        if (_registrar.FindUnit(code) is not null)
        {
            ErrorMessage = $"A unit with code {code} already exists.";
            LoadDashboardData();
            View = "units";
            return Page();
        }

        _registrar.Units.Add(new Unit
        {
            UnitCode = code,
            Title = NewUnitTitle.Trim(),
            Course = NewUnitCourse.Trim(),
            CreditPoints = NewUnitCreditPoints <= 0 ? 15 : NewUnitCreditPoints,
        });

        StatusMessage = $"Added unit {code} — {NewUnitTitle}.";
        return RedirectToPage(new { view = "units" });
    }

    public IActionResult OnPostEnrolStudent()
    {
        if (!RequireLogin(out var redirect)) return redirect!;

        var student = _registrar.FindStudent(EnrolStudentId.Trim());
        var unit = _registrar.FindUnit(EnrolUnitCode.Trim().ToUpperInvariant());

        if (student is null)
        {
            ErrorMessage = $"No student with id {EnrolStudentId}.";
        }
        else if (unit is null)
        {
            ErrorMessage = $"No unit with code {EnrolUnitCode}.";
        }
        else
        {
            try
            {
                _registrar.EnrolStudent(student, unit, string.IsNullOrWhiteSpace(EnrolSemester) ? "2026-2" : EnrolSemester.Trim());
                StatusMessage = $"Enrolled {student.Name} ({student.StudentId}) in {unit.UnitCode}.";
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        LoadDashboardData();
        View = "enrol";
        return Page();
    }

    public IActionResult OnPostRecordGrade()
    {
        if (!RequireLogin(out var redirect)) return redirect!;

        try
        {
            LoggedInAdmin!.RecordGrade(_registrar, GradeStudentId, GradeUnitCode, GradeMark);
            var student = _registrar.FindStudent(GradeStudentId);
            StatusMessage = student is null
                ? "Grade recorded."
                : $"Recorded {GradeMark} for {GradeStudentId} in {GradeUnitCode} — new WAM: {student.Wam:0.0}.";
        }
        catch (InvalidMarkException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (KeyNotFoundException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }

        LoadDashboardData();
        View = "grade";
        return Page();
    }

    // ---- helpers ----

    private bool RequireLogin(out IActionResult? redirect)
    {
        LoadLoggedInAdmin();
        if (LoggedInAdmin is null)
        {
            redirect = RedirectToPage();
            return false;
        }
        redirect = null;
        return true;
    }

    private void LoadLoggedInAdmin()
    {
        var staffId = HttpContext.Session.GetString(SessionKey);
        if (staffId is not null)
        {
            LoggedInAdmin = _registrar.FindAdministrator(staffId);
        }
    }

    private void LoadDashboardData()
    {
        TotalStudents = _registrar.Students.Count;
        TotalUnits = _registrar.Units.Count;
        TotalEnrolments = _registrar.Students.Sum(s => s.Enrolments.Count);
        AllStudents = _registrar.Students.OrderBy(s => s.Name).ToList();
        AllUnits = _registrar.Units.OrderBy(u => u.UnitCode).ToList();
        CourseNames = _registrar.Units.Select(u => u.Course).Distinct().OrderBy(c => c).ToList();
    }

    private string GenerateUniqueStudentId()
    {
        string id;
        do
        {
            id = "10" + IdRandom.Next(0, 1_000_000).ToString("D6");
        } while (_registrar.FindStudent(id) is not null);
        return id;
    }
}
