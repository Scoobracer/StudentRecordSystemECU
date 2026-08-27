using System.Text.Json;
using StudentRecordSystem.Models;

namespace StudentRecordSystem.Data;

/// <summary>
/// Persists student records to a local JSON file using System.Text.Json,
/// which ships with .NET and needs no external database or NuGet package.
/// This satisfies the "database or data file integration" requirement
/// while keeping the demo runnable in any plain .NET environment.
/// </summary>
public class JsonStudentRepository : IStudentRepository
{
    private readonly string _filePath;
    private List<Student> _students;

    public JsonStudentRepository(string filePath)
    {
        _filePath = filePath;
        _students = LoadFromDisk();
    }

    public List<Student> GetAll() => _students;

    public Student? GetById(string studentId) =>
        _students.FirstOrDefault(s => s.StudentId == studentId);

    public void Add(Student student)
    {
        _students.Add(student);
        SaveToDisk();
    }

    public void Update(Student student)
    {
        int index = _students.FindIndex(s => s.StudentId == student.StudentId);
        if (index >= 0)
        {
            _students[index] = student;
            SaveToDisk();
        }
    }

    private List<Student> LoadFromDisk()
    {
        if (!File.Exists(_filePath))
            return new List<Student>();

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
    }

    private void SaveToDisk()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_students, options);
        File.WriteAllText(_filePath, json);
    }
}
