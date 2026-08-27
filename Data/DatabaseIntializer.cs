// Data/DatabaseInitializer.cs
using Microsoft.Data.Sqlite;

public static class DatabaseInitializer
{
    public static void Initialize(string connectionString)
    {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Students (
                StudentId TEXT PRIMARY KEY,
                FirstName TEXT NOT NULL,
                LastName  TEXT NOT NULL,
                Email     TEXT
            );
            CREATE TABLE IF NOT EXISTS Courses (
                CourseCode TEXT PRIMARY KEY,
                Title      TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Units (
                UnitCode  TEXT PRIMARY KEY,
                UnitName  TEXT NOT NULL,
                Credits   INTEGER,
                CourseCode TEXT REFERENCES Courses(CourseCode)
            );
            CREATE TABLE IF NOT EXISTS Enrolments (
                EnrolmentId INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentId TEXT REFERENCES Students(StudentId),
                UnitCode  TEXT REFERENCES Units(UnitCode),
                Mark      REAL,
                Semester  TEXT
            );
            CREATE TABLE IF NOT EXISTS Users (
                Username     TEXT PRIMARY KEY,
                PasswordHash TEXT NOT NULL,
                Role         TEXT NOT NULL,
                LinkedStudentId TEXT
            );";
        cmd.ExecuteNonQuery();
    }
}