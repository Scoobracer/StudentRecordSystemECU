// Data/SqlStudentRepository.cs
using Microsoft.Data.Sqlite;
using StudentRecordSystem.Models;


namespace StudentRecordSystem.Data;



public class SqlStudentRepository : IStudentRepository
{
    private readonly string _connectionString;

    public SqlStudentRepository(string connectionString)
    {
        _connectionString = connectionString;
        DatabaseInitializer.Initialize(connectionString);
    }

    public Student? GetById(string id)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT StudentId, FirstName, LastName, Email FROM Students WHERE StudentId = $id";
        cmd.Parameters.AddWithValue("$id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Student
            {
                StudentId = reader.GetString(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.IsDBNull(3) ? "" : reader.GetString(3)
            };
        }
        return null;
    }

    public List<Student> GetAll()
    {
        var students = new List<Student>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT StudentId, FirstName, LastName, Email FROM Students";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            students.Add(new Student
            {
                StudentId = reader.GetString(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.IsDBNull(3) ? "" : reader.GetString(3)
            });
        }
        return students;
    }

    public void Add(Student s)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Students (StudentId, FirstName, LastName, Email) VALUES ($id, $fn, $ln, $em)";
        cmd.Parameters.AddWithValue("$id", s.StudentId);
        cmd.Parameters.AddWithValue("$fn", s.FirstName);
        cmd.Parameters.AddWithValue("$ln", s.LastName);
        cmd.Parameters.AddWithValue("$em", s.Email);
        cmd.ExecuteNonQuery();
    }

    public void Update(Student s)
{
    using var conn = new SqliteConnection(_connectionString);
    conn.Open();
    var cmd = conn.CreateCommand();
    cmd.CommandText = @"
        UPDATE Students
        SET FirstName = $fn, LastName = $ln, Email = $em
        WHERE StudentId = $id";
    cmd.Parameters.AddWithValue("$fn", s.FirstName);
    cmd.Parameters.AddWithValue("$ln", s.LastName);
    cmd.Parameters.AddWithValue("$em", s.Email);
    cmd.Parameters.AddWithValue("$id", s.StudentId);
    cmd.ExecuteNonQuery();
}
}