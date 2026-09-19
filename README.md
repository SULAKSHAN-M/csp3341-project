# Student Record Management System — Web (2026 Modernised Edition)

This is a modernised, browser-based companion to the console application from
the Technical Report appendix. It uses the same domain model (Person, Student,
Administrator, Unit, Enrolment, Registrar) implemented as an ASP.NET Core
Razor Pages application, seeded with:

- The full 2026 ECU unit catalogue supplied (156 units across 9 Bachelor
  programs: Biomedical Science, Commerce, Computer Science, Communication,
  Science (Cyber Security), Design, Psychology, Technology/Engineering —
  Electrical, and Science (Nursing Studies)).
- 10 sample students, each with a student ID in the real ECU format —
  starting with "10" followed by six more digits (e.g. `10730907`,
  `10812234`, ...) — enrolled in units from their own course and given
  recorded grades so WAM and transcripts populate immediately.
- The ECU logo in the site header (from the high-resolution file you supplied).

## Requirements

- .NET 10 SDK (or edit `StudentRecordManagement.Web.csproj` and change
  `<TargetFramework>net10.0</TargetFramework>` to whatever SDK you have, e.g.
  `net8.0` — nothing here needs .NET 10 specifically).

## How to run it

```
cd StudentRecordManagement.Web
dotnet restore
dotnet run
```

Then open the URL shown in the terminal (defaults to
`http://localhost:5080`) — it should open automatically in your browser.

## Pages

- **Dashboard** (`/`) — total units, programs, students, average WAM, a
  leaderboard of top students by WAM, and a per-program unit count.
- **Unit Catalogue** (`/Units`) — every seeded unit, grouped by Bachelor
  program, with a search box (searches code, title, and course name).
- **Students** (`/Students`) — the full student roster; click a student ID
  to see their individual transcript (unit, semester, mark, grade).
- **Transcript Lookup** (`/Transcript`) — a self-service page: type in any
  student ID (e.g. `10730907`) to view that student's transcript and WAM
  directly, without browsing the full roster.
- **Administrator Login** (`/Login`) — signs in as a seeded administrator
  (demo credentials: Staff ID `STF0001`, password `ecu2026`) and lands on a
  full **Administrator Dashboard** with:
  - live counts of students, units, and enrolments
  - **Register / view students** — add a new student (a unique ID starting
    with "10" is generated automatically) and browse the full roster
  - **Add / view units** — add a new unit to the catalogue and browse all
    seeded units
  - **Enrol a student** — enrol any student in any unit by ID/code
  - **Record a grade** — record a mark for an existing enrolment

  All four actions exercise the same `Registrar` validation, exception
  handling, and `GradeRecorded` event discussed in the Technical Report —
  try an out-of-range mark, a duplicate unit code, or enrolling into a
  unit twice to see the friendly error messages. The site header also
  shows "Signed in: {name}" with a Log out link whenever an administrator
  is logged in, on every page.

  **Security note:** the login uses a single hardcoded, plaintext password
  purely so the CSP3341 demonstration has something to check. This is
  explicitly flagged in the code (`Administrator.Password`) as something a
  real system must never do — production authentication should use ASP.NET
  Core Identity (or another provider) with salted, hashed passwords.

## Notes for your report / submission

- This web app is an additional, modernised front end — it is **not** a
  replacement for the console application already described in your
  Technical Report's Part B. If you want to reference it in the report,
  treat it as an extension built on the same domain model, and take fresh
  screenshots of these pages (Dashboard, Unit Catalogue, and a student
  transcript) alongside the console output already discussed.
- All student IDs are seeded in code (`Data/SeedData.cs`) — edit that file
  if you want to add, remove, or rename students or units.
- I was not able to compile or run this myself (no .NET SDK or the right
  network access in the sandbox I used to build it) — please run
  `dotnet build` first and send me any compiler errors so I can fix them
  immediately.
