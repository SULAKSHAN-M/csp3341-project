namespace StudentRecordManagement.Web.Domain;

// A unit of study. Extended with Course to support the 2026 modernised
// catalogue view, which groups units by the Bachelor program they belong to.
public class Unit
{
    public string UnitCode { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Course { get; init; } = string.Empty;    // e.g. "Bachelor of Computer Science"
    public int CreditPoints { get; init; } = 15;           // ECU standard unit weighting
}
