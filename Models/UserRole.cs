namespace StudentRecordSystem.Models;

/// <summary>
/// Distinguishes the two access levels supported by the system.
/// Admin users can manage every student record; Student users can
/// only view their own record.
/// </summary>
public enum UserRole
{
    Admin,
    Student
}
