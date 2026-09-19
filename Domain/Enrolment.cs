namespace StudentRecordManagement.Web.Domain;

public class Enrolment
{
    public string EnrolmentId { get; init; } = string.Empty;
    public string UnitCode { get; init; } = string.Empty;
    public string Semester { get; init; } = string.Empty;
    public int Mark { get; private set; }
    public string Grade { get; set; } = "NA";

    public void RecordGrade(int mark)
    {
        if (mark < 0 || mark > 100)
            throw new InvalidMarkException(mark);
        Mark = mark;
    }

    public double CalculateGradePoint() => Mark switch
    {
        >= 80 => 4.0, >= 70 => 3.0, >= 60 => 2.0, >= 50 => 1.0, _ => 0.0
    };
}
