namespace StudentRecordManagement.Web.Domain;

public class Administrator : Person
{
    public string StaffId => PersonId;
    public List<string> Permissions { get; } = new();

    // DEMO ONLY: a real system must never store or check plaintext passwords —
    // use ASP.NET Core Identity (or another provider) with salted password
    // hashes. This field exists purely so the Login page has something simple
    // to validate for the CSP3341 demonstration.
    public string Password { get; init; } = string.Empty;

    public void RegisterStudent(Registrar registrar, Student student) =>
        registrar.Students.Add(student);

    public void RecordGrade(Registrar registrar, string studentId, string unitCode, int mark) =>
        registrar.RecordGrade(studentId, unitCode, mark);
}
