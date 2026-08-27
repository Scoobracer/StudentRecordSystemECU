namespace StudentRecordSystem.Models;

/// <summary>
/// Represents a single unit a student is enrolled in or has completed.
/// One Unit instance is effectively one enrolment record: it carries
/// both the unit's details and the student's mark for that unit.
/// </summary>
public class Unit
{
    public string UnitCode { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
    public int Credits { get; set; }
    public double Mark { get; set; }          // 0-100, 0 = not yet graded
    public string Semester { get; set; } = string.Empty;

    /// <summary>Simple derived letter grade, used for display only.</summary>
    public string Grade => Mark switch
    {
        >= 80 => "HD",
        >= 70 => "D",
        >= 60 => "C",
        >= 50 => "P",
        > 0 => "N",
        _ => "-"
    };

    public override string ToString() =>
        $"{UnitCode,-10} {UnitName,-22} {Semester,-10} Mark: {Mark,5:0.0}  Grade: {Grade}";
}
