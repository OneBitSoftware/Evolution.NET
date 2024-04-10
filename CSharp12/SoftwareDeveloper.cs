namespace CSharp12;

using System.Collections.Immutable;

public class SoftwareDeveloper(string firstName, string lastName, ImmutableList<string> projects)
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public IReadOnlyCollection<string> Projects { get; } = projects;
}