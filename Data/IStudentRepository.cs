using StudentRecordSystem.Models;

namespace StudentRecordSystem.Data;

/// <summary>
/// Abstracts data access away from the rest of the application.
/// The console UI and services only ever depend on this interface,
/// so the storage technology (JSON file today, a database later)
/// can change without touching any other code.
/// </summary>
public interface IStudentRepository
{
    List<Student> GetAll();
    Student? GetById(string studentId);
    void Add(Student student);
    void Update(Student student);
}
