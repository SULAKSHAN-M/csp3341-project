namespace StudentRecordManagement.Web.Domain;

// Abstract root of the Person hierarchy.
public abstract class Person
{
    public string PersonId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Email { get; init; }

    public virtual string GetDetails() => $"{PersonId}: {Name}";
}
