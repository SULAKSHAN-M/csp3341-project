namespace StudentRecordManagement.Web.Domain;

public class Student : Person, ITranscriptGenerator
{
    public string StudentId => PersonId;
    public string Course { get; init; } = string.Empty;   // enrolled degree program
    public List<Enrolment> Enrolments { get; } = new();
    public double Wam { get; private set; }

    public double CalculateWam()
    {
        var completed = Enrolments.Where(e => e.Grade != "NA").ToList();
        Wam = completed.Count == 0 ? 0.0 : completed.Average(e => e.Mark);
        return Wam;
    }

    public string GetTranscript() =>
        string.Join(Environment.NewLine,
            Enrolments.Select(e => $"{e.UnitCode,-10}{e.Grade,-4}{e.Mark,4}"));

    public string GenerateReport() => GetTranscript();
}
