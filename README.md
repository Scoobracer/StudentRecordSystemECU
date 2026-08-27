# Student Record System — CSP3341 Preliminary Build

A minimal, working C# console application implementing the class design
in the Preliminary Report (Figure 1). It demonstrates role-based access,
automatic WAM calculation, and file-based persistence using only the
.NET base class library — no external NuGet packages required, so it
builds offline.

## Requirements
- .NET 8 SDK (https://dotnet.microsoft.com/download)
- Visual Studio 2022, VS Code + C# Dev Kit, or Rider

## How to run
```
cd StudentRecordSystem
dotnet run
```

## Demo logins
| Username | Password | Role    | Sees                                  |
|----------|----------|---------|----------------------------------------|
| admin    | admin123 | Admin   | All students; can add students/enrolments |
| s12345   | pass123  | Student | Only their own record (Aisha Perera)  |

## What to screenshot for Part B evidence
1. Terminal running `dotnet --version` — proves the SDK is installed.
2. This project running in your IDE with the console output visible
   (log in as `admin`, choose option 1 to list students) — proves the
   code compiles and runs.
3. Your GitHub repository page after pushing this project.

## Project structure
```
StudentRecordSystem/
├── StudentRecordSystem.csproj
├── Program.cs                  # console entry point, role-based menus
├── Models/
│   ├── UserRole.cs             # enum: Admin, Student
│   ├── Unit.cs                 # one enrolled/completed unit + mark
│   ├── Student.cs              # student + CalculateWam()
│   └── User.cs                 # login account
├── Data/
│   ├── IStudentRepository.cs   # repository pattern interface
│   └── JsonStudentRepository.cs# JSON-file-backed implementation
└── Services/
    └── AuthService.cs          # login/authentication
```

## Extending it later
- Swap `JsonStudentRepository` for a `SqlStudentRepository` (e.g. with
  EF Core + SQLite) without changing `Program.cs`, because everything
  depends on the `IStudentRepository` interface, not the concrete class.
- Replace the console UI with a WPF or MAUI front end; the Models,
  Data and Services layers do not need to change.
- Hash passwords (e.g. with `BCrypt.Net-Next`) instead of the plain-text
  comparison used here for demo simplicity.
