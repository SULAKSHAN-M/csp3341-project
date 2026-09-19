namespace StudentRecordManagement.Web.Domain;

public record GradeEventArgs(string StudentId, string UnitCode, int Mark, string Grade);

// Central coordinator: owns the units catalogue and the students, enrols
// students, validates and records grades, and notifies subscribers when it does.
public class Registrar
{
    public string Name { get; init; } = string.Empty;
    public List<Student> Students { get; } = new();
    public List<Unit> Units { get; } = new();
    public List<Administrator> Administrators { get; } = new();

    public event EventHandler<GradeEventArgs>? GradeRecorded;

    public Student? FindStudent(string id) =>
        Students.FirstOrDefault(s => s.StudentId == id);

    public Unit? FindUnit(string unitCode) =>
        Units.FirstOrDefault(u => u.UnitCode == unitCode);

    public Administrator? FindAdministrator(string staffId) =>
        Administrators.FirstOrDefault(a => a.StaffId == staffId);

    public Enrolment EnrolStudent(Student student, Unit unit, string semester = "2026-2")
    {
        if (student.Enrolments.Any(e => e.UnitCode == unit.UnitCode))
            throw new InvalidOperationException(
                $"{student.StudentId} is already enrolled in {unit.UnitCode}.");

        var enrolment = new Enrolment
        {
            EnrolmentId = Guid.NewGuid().ToString("N")[..8],
            UnitCode = unit.UnitCode,
            Semester = semester
        };
        student.Enrolments.Add(enrolment);
        return enrolment;
    }

    public void RecordGrade(string studentId, string unitCode, int mark)
    {
        if (mark < 0 || mark > 100)
            throw new InvalidMarkException(mark);

        var student = FindStudent(studentId)
            ?? throw new KeyNotFoundException($"No student with id {studentId}.");
        var enrolment = student.Enrolments.FirstOrDefault(e => e.UnitCode == unitCode)
            ?? throw new InvalidOperationException(
                $"{studentId} is not enrolled in {unitCode}.");

        enrolment.RecordGrade(mark);
        student.CalculateWam();

        string grade = mark switch
        {
            >= 80 => "HD", >= 70 => "D", >= 60 => "C", >= 50 => "P", _ => "N"
        };
        enrolment.Grade = grade;

        GradeRecorded?.Invoke(this, new GradeEventArgs(studentId, unitCode, mark, grade));
    }

    public async Task RecordGradesAsync(
        IEnumerable<(string StudentId, string UnitCode, int Mark)> marks)
    {
        foreach (var (studentId, unitCode, mark) in marks)
        {
            try
            {
                RecordGrade(studentId, unitCode, mark);
            }
            catch (InvalidMarkException)
            {
                continue;
            }
            await Task.Delay(1);
        }
    }
}
