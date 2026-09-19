# Student Record Management System

## CSP3341 – Programming Languages and Paradigms

A browser-based **Student Record Management System** developed using **C#**, **ASP.NET Core Razor Pages**, and object-oriented programming principles.

This application was created as the software demonstration component of the **CSP3341: Programming Languages and Paradigms Technical Report** at Edith Cowan University.

The project demonstrates the practical application of C# language features, object-oriented programming principles, exception handling, event handling, asynchronous programming, validation, and structured software design.

The web application extends the original console-based implementation while maintaining the same core domain model, including `Person`, `Student`, `Administrator`, `Unit`, `Enrolment`, and `Registrar`.

---

## Academic Context

**Unit:** CSP3341 – Programming Languages and Paradigms  
**Assessment:** Technical Report  
**Programming Language:** C#  
**Application:** Student Record Management System  
**Institution:** Edith Cowan University  
**Purpose:** Software demonstration and programming language analysis

The application was developed to support the technical investigation of C# by providing practical examples of the language concepts discussed in the accompanying technical report.

The implementation demonstrates concepts including:

- Naming conventions
- Data types
- Expressions and assignment statements
- Statement-level control structures
- Methods and subprograms
- Abstract data types
- Encapsulation
- Object-oriented programming
- Inheritance
- Exception handling
- Event handling
- Asynchronous programming
- Readability and writability
- Structured application design

---

## Project Overview

The Student Record Management System provides a browser-based interface for managing academic information.

The system supports:

- Student management
- Academic unit management
- Student enrolments
- Marks and grades
- Weighted Average Mark calculations
- Academic transcripts
- Administrator operations
- Academic statistics

The application includes preloaded demonstration data so that the main functionality can be explored immediately after startup.

---

## Features

### Dashboard

The main dashboard provides an overview of the academic system.

It displays:

- Total students
- Total academic units
- Total academic programs
- Average student WAM
- Top-performing students by WAM
- Unit distribution by academic program

---

### Student Management

Administrators can:

- View registered students
- Register new students
- Automatically generate unique student IDs
- View individual student details
- Access student transcripts

Student IDs follow the ECU-style format beginning with `10` followed by six additional digits.

Example:

```text
10730907
```

---

### Unit Management

The system includes a searchable academic unit catalogue.

Administrators can:

- View available units
- Add new units
- Search by unit code
- Search by unit title
- Search by academic program

The demonstration dataset contains **156 units across nine Bachelor degree programs**.

---

### Student Enrolment

Administrators can enrol students into available academic units.

The system validates enrolment operations to prevent:

- Duplicate enrolments
- Invalid student IDs
- Invalid unit codes
- Re-enrolment in the same unit
- Invalid academic operations

---

### Grade Management

Administrators can record student marks for existing enrolments.

The system validates entered marks and prevents invalid values outside the accepted range.

Recording a grade also triggers the application's:

```csharp
GradeRecorded
```

event, demonstrating event-driven programming in C#.

---

### Automatic WAM Calculation

The system automatically calculates each student's **Weighted Average Mark (WAM)** using their recorded academic results.

WAM information is displayed on:

- Student profiles
- Student transcripts
- Main dashboard
- Administrator dashboard

---

### Transcript Generation

Each student has an individual academic transcript containing:

- Unit code
- Unit name
- Semester
- Mark
- Grade
- Weighted Average Mark

A dedicated **Transcript Lookup** page allows a transcript to be retrieved directly using a student ID.

---

## Administrator Portal

The system includes an administrator login and management dashboard.

### Demo Credentials

```text
Staff ID: STF0001
Password: ecu2026
```

After signing in, administrators can:

- Register students
- View students
- Add academic units
- View units
- Enrol students
- Record grades
- View system statistics
- Access student transcripts
- Log out securely from the current session

The navigation bar displays the currently signed-in administrator.

---

## Application Pages

| Page | Route | Description |
|---|---|---|
| Dashboard | `/` | Displays system statistics, WAM information and academic summaries |
| Unit Catalogue | `/Units` | Displays and searches available academic units |
| Students | `/Students` | Displays registered students |
| Student Transcript | Student-specific | Displays academic results for an individual student |
| Transcript Lookup | `/Transcript` | Finds a transcript using a student ID |
| Administrator Login | `/Login` | Provides administrator authentication |
| Administrator Dashboard | Admin area | Provides student, unit, enrolment and grade management |

---

## Sample Data

The project includes demonstration data so that the system can be explored immediately after startup.

The dataset contains:

- **156 academic units**
- **9 Bachelor degree programs**
- **10 sample students**
- Student enrolments
- Recorded grades
- Calculated WAM values
- Generated transcripts

The included academic programs cover areas such as:

- Biomedical Science
- Commerce
- Computer Science
- Communication
- Cyber Security
- Design
- Psychology
- Technology and Engineering
- Nursing Studies

---

## Technologies Used

The project uses the following technologies:

- C#
- .NET
- ASP.NET Core
- Razor Pages
- HTML
- CSS
- Object-Oriented Programming
- Event-Driven Programming
- Asynchronous Programming
- Server-Side Validation
- Session-Based Administrator Authentication

---

## Object-Oriented Design

The application demonstrates several core object-oriented programming concepts.

### Encapsulation

Student academic information and WAM calculation logic are controlled through domain classes rather than being directly manipulated by the user interface.

This helps maintain data integrity and ensures that business rules remain within the appropriate application layer.

---

### Inheritance

Shared information is represented through the `Person` base class.

Specialised classes extend this base class, including:

```text
Student
Administrator
```

This demonstrates inheritance, code reuse, and hierarchical object-oriented design.

---

### Abstraction

Academic operations are managed through domain classes rather than placing all business logic directly inside the web interface.

The main domain classes include:

```text
Person
Student
Administrator
Unit
Enrolment
Registrar
```

This provides separation between the application's business logic and its presentation layer.

---

### Validation

The `Registrar` class manages important academic operations and validation, including:

- Student registration
- Unit creation
- Student enrolment
- Grade recording

This centralises important business rules within the application.

---

### Event Handling

The application includes a:

```csharp
GradeRecorded
```

event.

This event demonstrates event-driven programming when a student's academic result is recorded.

---

### Exception Handling

The application uses exception handling and validation to manage invalid operations.

Examples include:

- Duplicate unit codes
- Duplicate student enrolments
- Invalid marks
- Invalid student IDs
- Invalid unit codes
- Missing students
- Missing units
- Invalid academic operations

Instead of displaying raw technical exceptions directly to users, the web interface provides understandable error messages.

---

### Asynchronous Programming

The project demonstrates asynchronous programming using C# features such as:

```csharp
async
await
Task
```

These features allow asynchronous operations to be handled without unnecessarily blocking the application's execution flow.

---

## C# Concepts Demonstrated

The Student Record Management System was designed as both a functional application and a practical demonstration of C#.

The project demonstrates:

- Classes and objects
- Properties
- Constructors
- Methods
- Encapsulation
- Inheritance
- Abstraction
- Static typing
- Control structures
- Exception handling
- Event handling
- Asynchronous programming
- Validation
- Separation of concerns
- Reusable domain models

---

## Assignment Relevance

This application was developed as the practical software component of the **CSP3341 Technical Report**.

The purpose of the implementation is to demonstrate the C# programming language through a working software system and provide practical examples that can be analysed in the accompanying report.

The application supports discussion of C# in areas such as:

- Object-oriented programming
- Type safety
- Encapsulation
- Exception handling
- Event handling
- Asynchronous programming
- Structured program design
- Readability
- Writability
- Maintainability
- Performance considerations

The accompanying technical report discusses both positive and less favourable aspects of C# based on the experience of developing this application.

---

## Positive C# Characteristics Demonstrated

The application provides practical examples of several useful characteristics of C#, including:

- Strong support for object-oriented programming
- Static typing and type safety
- Structured exception handling
- Properties and encapsulation
- Event-driven programming
- Asynchronous programming support
- Integration with the .NET ecosystem
- Clear separation between business logic and presentation
- Strong tooling and framework support

---

## C# Challenges Considered

The project also provides examples that can be used to discuss some challenges associated with C#, including:

- Verbose syntax in certain implementations
- Additional boilerplate associated with strongly typed class structures
- Complexity introduced by asynchronous control flow
- Learning requirements associated with the wider .NET ecosystem

These areas are discussed in greater detail in the accompanying CSP3341 Technical Report.

---

## Project Structure

A simplified representation of the project structure is shown below:

```text
StudentRecordManagement/
│
├── StudentRecordManagement.Web/
│   ├── Models/
│   ├── Pages/
│   ├── Services/
│   ├── wwwroot/
│   ├── Program.cs
│   └── StudentRecordManagement.Web.csproj
│
├── README.md
│
└── Additional project files
```

---

## Requirements

Install the **.NET SDK** before running the project.

The supplied project targets:

```text
.NET 10
```

The target framework can be found in:

```text
StudentRecordManagement.Web.csproj
```

The project currently uses:

```xml
<TargetFramework>net10.0</TargetFramework>
```

If .NET 10 is unavailable, the target framework can be changed to a compatible installed version such as:

```xml
<TargetFramework>net8.0</TargetFramework>
```

provided that the project dependencies remain compatible.

---

## Running the Project

### 1. Clone the Repository

```bash
git clone https://github.com/SULAKSHAN-M/csp3341-project.git
```

### 2. Navigate to the Project Directory

```bash
cd csp3341-project
```

### 3. Open the Web Application Directory

```bash
cd StudentRecordManagement.Web
```

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Run the Application

```bash
dotnet run
```

### 6. Open the Application

Open the URL displayed in the terminal.

The default development URL is typically:

```text
http://localhost:5080
```

---

## Validation and Error Handling

The system includes validation and exception handling for important academic operations.

Validation is applied to:

- Student registration
- Unit creation
- Student enrolment
- Grade recording
- Duplicate unit codes
- Duplicate student enrolments
- Invalid marks
- Missing students
- Missing units
- Invalid academic operations

The web application presents user-friendly validation messages rather than exposing raw application exceptions.

---

## Security Notice

The administrator account uses a hardcoded demonstration password.

```text
Staff ID: STF0001
Password: ecu2026
```

This implementation is included only for academic demonstration purposes.

A production system should **never store passwords in plaintext**.

A real-world implementation should use secure authentication mechanisms such as:

- ASP.NET Core Identity
- Salted password hashing
- Database-backed user accounts
- Role-based authorization
- Secure session management
- Authentication and authorization middleware

---

## Project Purpose

This project was developed to demonstrate the practical application of:

- C# programming
- Object-oriented programming
- ASP.NET Core web development
- Razor Pages
- Academic data management
- Form validation
- Exception handling
- Event-driven programming
- Asynchronous programming
- Separation of business logic and presentation
- Software design principles

The application converts a traditional console-based Student Record Management System into a browser-based interface while maintaining the original domain architecture and business rules.

---

## Future Improvements

Potential future enhancements include:

- ASP.NET Core Identity authentication
- Database persistence
- Entity Framework Core integration
- Role-based access control
- Student login accounts
- Online unit registration
- Transcript PDF export
- Advanced academic reporting
- Grade analytics
- REST API support
- Audit logging
- Improved responsive design
- Persistent administrator accounts

---

## Academic Purpose

This repository contains the software demonstration developed for:

**CSP3341 – Programming Languages and Paradigms**  
**Technical Report – C# Language Investigation**

The application provides practical evidence of the development and implementation of a software system using C#.

It is intended to demonstrate selected C# programming language features and support the technical discussion presented in the accompanying CSP3341 Technical Report.

---

## Author

**Marudanayagam Sulakshan**

Software Engineering Undergraduate  
Edith Cowan University Colombo

Areas of interest:

- Software Engineering
- Full-Stack Development
- C# and .NET Development
- UI/UX Design
- Cloud Technologies
- Artificial Intelligence

---

## Academic Disclaimer

This repository was created primarily for educational and academic purposes as part of the **CSP3341 Programming Languages and Paradigms** assessment.

The source code, documentation, and related materials are provided for educational and portfolio demonstration purposes and should not be submitted by another student as their own academic work.

---

## License

This project was developed primarily for **educational, academic, and portfolio demonstration purposes**.
