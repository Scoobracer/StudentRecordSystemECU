namespace StudentRecordSystem.Models;

/// <summary>
/// The core domain entity: a student and the units they are enrolled in.
/// </summary>
public class Student
{
    public string StudentId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string CourseCode { get; set; } = string.Empty;
    public List<Unit> Units { get; set; } = new();

    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Calculates the Weighted Average Mark across every graded unit,
    /// weighting each mark by that unit's credit points. Ungraded
    /// units (Mark == 0) are excluded so in-progress enrolments don't
    /// drag the average down.
    /// </summary>
    public double CalculateWam()
    {
        var graded = Units.Where(u => u.Mark > 0).ToList();
        if (graded.Count == 0) return 0;

        double totalWeightedMark = graded.Sum(u => u.Mark * u.Credits);
        double totalCredits = graded.Sum(u => u.Credits);

        return totalCredits == 0 ? 0 : Math.Round(totalWeightedMark / totalCredits, 2);
    }
}
