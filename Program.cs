using StudentRecordSystem.Data;
using StudentRecordSystem.Models;
using StudentRecordSystem.Services;

namespace StudentRecordSystem;

internal class Program
{
    private static readonly IStudentRepository Repo = new JsonStudentRepository("students.json");

    // Demo accounts. "admin" can manage every record; "s12345" can
    // only view the student record it is linked to.
    private static readonly List<User> Users = new()
    {
        new User { Username = "admin",  PasswordHash = "admin123", Role = UserRole.Admin },
        new User { Username = "s12345", PasswordHash = "pass123",  Role = UserRole.Student, LinkedStudentId = "s12345" },
    };

    private static readonly AuthService Auth = new(Users);

    private static void Main()
    {
        SeedDemoDataIfEmpty();

        Console.WriteLine("=== Student Record System ===");
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? string.Empty;
        Console.Write("Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        User? user = Auth.Login(username, password);
        if (user is null)
        {
            Console.WriteLine("Invalid username or password.");
            return;
        }

        if (user.Role == UserRole.Admin)
            RunAdminMenu();
        else
            ShowOwnRecord(user);
    }

    private static void SeedDemoDataIfEmpty()
    {
        if (Repo.GetAll().Count > 0) return;

        var student = new Student
        {
            StudentId = "s12345",
            FirstName = "Aisha",
            LastName = "Perera",
            DateOfBirth = new DateTime(2003, 4, 12),
            Email = "aisha.perera@student.ecu.edu.au",
            CourseCode = "BSC-IT",
            Units = new List<Unit>
            {
                new() { UnitCode = "CSP1150", UnitName = "Programming 1",   Credits = 15, Mark = 78, Semester = "S1 2025" },
                new() { UnitCode = "CSP2101", UnitName = "Data Structures", Credits = 15, Mark = 82, Semester = "S2 2025" },
            }
        };

        Repo.Add(student);
    }

    // ---------------- Admin flows ----------------

    private static void RunAdminMenu()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("1. View all students");
            Console.WriteLine("2. Add new student");
            Console.WriteLine("3. Enrol a student in a unit");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1": ListAllStudents(); break;
                case "2": AddStudent(); break;
                case "3": EnrolStudentInUnit(); break;
                case "4": running = false; break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void ListAllStudents()
    {
        Console.WriteLine("\nStudentId  Name                 Course      WAM");
        foreach (Student s in Repo.GetAll())
            Console.WriteLine($"{s.StudentId,-10} {s.FullName,-20} {s.CourseCode,-10} {s.CalculateWam(),6:0.00}");
    }

    private static void AddStudent()
    {
        Console.Write("Student ID: ");
        string id = Console.ReadLine() ?? string.Empty;
        Console.Write("First name: ");
        string first = Console.ReadLine() ?? string.Empty;
        Console.Write("Last name: ");
        string last = Console.ReadLine() ?? string.Empty;
        Console.Write("Course code: ");
        string course = Console.ReadLine() ?? string.Empty;

        var student = new Student
        {
            StudentId = id,
            FirstName = first,
            LastName = last,
            CourseCode = course,
            DateOfBirth = DateTime.Today,
            Email = $"{id}@student.ecu.edu.au"
        };

        Repo.Add(student);
        Console.WriteLine("Student added and saved to students.json.");
    }

    private static void EnrolStudentInUnit()
    {
        Console.Write("Student ID: ");
        string id = Console.ReadLine() ?? string.Empty;
        Student? student = Repo.GetById(id);
        if (student is null)
        {
            Console.WriteLine("No student found with that ID.");
            return;
        }

        Console.Write("Unit code: ");
        string unitCode = Console.ReadLine() ?? string.Empty;
        Console.Write("Unit name: ");
        string unitName = Console.ReadLine() ?? string.Empty;
        Console.Write("Credits: ");
        int credits = int.TryParse(Console.ReadLine(), out int c) ? c : 15;
        Console.Write("Mark (0-100, leave 0 if not yet graded): ");
        double mark = double.TryParse(Console.ReadLine(), out double m) ? m : 0;

        student.Units.Add(new Unit
        {
            UnitCode = unitCode,
            UnitName = unitName,
            Credits = credits,
            Mark = mark,
            Semester = "Current"
        });

        Repo.Update(student);
        Console.WriteLine($"Enrolment added. {student.FullName}'s WAM is now {student.CalculateWam():0.00}.");
    }

    // ---------------- Student flow ----------------

    private static void ShowOwnRecord(User user)
    {
        Student? student = Repo.GetById(user.LinkedStudentId ?? string.Empty);
        if (student is null)
        {
            Console.WriteLine("No student record is linked to this account.");
            return;
        }

        Console.WriteLine($"\nWelcome, {student.FullName}");
        Console.WriteLine($"Student ID : {student.StudentId}");
        Console.WriteLine($"Course     : {student.CourseCode}");
        Console.WriteLine($"Email      : {student.Email}");
        Console.WriteLine("\nUnit History:");
        foreach (Unit u in student.Units)
            Console.WriteLine($"  {u}");
        Console.WriteLine($"\nCurrent WAM: {student.CalculateWam():0.00}");
    }
}
