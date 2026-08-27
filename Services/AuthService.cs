using StudentRecordSystem.Models;

namespace StudentRecordSystem.Services;

/// <summary>
/// Handles login. Passwords are compared in plain text here purely to
/// keep this preliminary demo self-contained; a real build should hash
/// passwords (e.g. with BCrypt.Net) before storing or comparing them.
/// </summary>
public class AuthService
{
    private readonly List<User> _users;

    public AuthService(List<User> users)
    {
        _users = users;
    }

    public User? Login(string username, string password) =>
        _users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
}
