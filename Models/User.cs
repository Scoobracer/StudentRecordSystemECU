namespace StudentRecordSystem.Models;

/// <summary>
/// A login account. Admin accounts have LinkedStudentId == null;
/// Student accounts are linked to exactly one Student record.
/// </summary>
public class User
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? LinkedStudentId { get; set; }
}
