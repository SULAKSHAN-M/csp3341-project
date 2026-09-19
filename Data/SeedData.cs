using StudentRecordManagement.Web.Domain;

namespace StudentRecordManagement.Web.Data;

// Seeds the Registrar with the 2026 unit catalogue (drawn from the ECU course
// guide) and a set of sample students whose IDs all start with "10", matching
// the format of a real ECU student number (e.g. 10730907).
public static class SeedData
{
    public static Registrar Build()
    {
        var registrar = new Registrar { Name = "ECU Registrar" };

        foreach (var (code, title, course) in Catalogue)
        {
            registrar.Units.Add(new Unit { UnitCode = code, Title = title, Course = course });
        }

        foreach (var (id, name, course, enrolledUnits) in Students)
        {
            var student = new Student { PersonId = id, Name = name, Course = course };
            registrar.Students.Add(student);

            foreach (var (unitCode, mark) in enrolledUnits)
            {
                var unit = registrar.FindUnit(unitCode);
                if (unit is null) continue;

                registrar.EnrolStudent(student, unit);
                registrar.RecordGrade(student.StudentId, unitCode, mark);
            }
        }

        // Seed one administrator account for the Login demo. Staff IDs use a
        // different format from student IDs (they don't start with "10") so
        // the two account types stay visibly distinct in the system.
        registrar.Administrators.Add(new Administrator
        {
            PersonId = "STF0001",
            Name = "Dr Leisa Armstrong",
            Password = "ecu2026",   // DEMO ONLY — see the comment on Administrator.Password
        });
        registrar.Administrators[0].Permissions.Add("CanRecordGrades");

        return registrar;
    }

    // (UnitCode, Title, Course)
    private static readonly (string, string, string)[] Catalogue = new (string, string, string)[]
    {
        // ---- Bachelor of Biomedical Science ----
        ("SCH1101", "Society Health and Culture", "Bachelor of Biomedical Science"),
        ("MHS1101", "Anatomy and Physiology 1", "Bachelor of Biomedical Science"),
        ("MAT1114", "Introduction to Statistics", "Bachelor of Biomedical Science"),
        ("SCC1123", "Chemistry for the Life Sciences", "Bachelor of Biomedical Science"),
        ("SCH1133", "Human Genetics", "Bachelor of Biomedical Science"),
        ("SCC1226", "Introduction to Organic Chemistry and Biochemistry", "Bachelor of Biomedical Science"),
        ("MHS1102", "Anatomy and Physiology 2", "Bachelor of Biomedical Science"),
        ("SCH1104", "Introduction to Pathophysiology", "Bachelor of Biomedical Science"),
        ("SCH1132", "Human Evolution and Ecology", "Bachelor of Biomedical Science"),
        ("SCH2235", "Applied Microbiology", "Bachelor of Biomedical Science"),
        ("SCH1111", "Fundamental Biomedical Techniques", "Bachelor of Biomedical Science"),
        ("SCH3145", "Biomedical Ethics", "Bachelor of Biomedical Science"),
        ("SCH3239", "Human Immunology", "Bachelor of Biomedical Science"),
        ("SCH2111", "Applied Physiology", "Bachelor of Biomedical Science"),
        ("SCH2232", "Medical Biochemistry", "Bachelor of Biomedical Science"),
        ("SCH2105", "Principles of Pharmacology", "Bachelor of Biomedical Science"),
        ("SCH2142", "Forensic Genetics", "Bachelor of Biomedical Science"),
        ("SCH2226", "Human Molecular Genetics", "Bachelor of Biomedical Science"),
        ("SCH2141", "Advanced Biomedical Techniques", "Bachelor of Biomedical Science"),
        ("SCH3227", "The Biology of Human Disease", "Bachelor of Biomedical Science"),
        ("SCH3244", "Developmental Biology", "Bachelor of Biomedical Science"),
        ("MMS3101", "Evolutionary Perspectives on Health and Disease", "Bachelor of Biomedical Science"),
        ("SCH3223", "Medical Genetics", "Bachelor of Biomedical Science"),
        ("SCH3434", "Human Reproduction, Development and Ageing", "Bachelor of Biomedical Science"),
        ("MMS3500", "Professional Practice in Biomedical Science", "Bachelor of Biomedical Science"),

        // ---- Bachelor of Commerce ----
        ("SBL1100", "Foundations of Business", "Bachelor of Commerce"),
        ("SBL1200", "Accounting and Finance Essentials", "Bachelor of Commerce"),
        ("SBL1300", "Business Environments & Markets", "Bachelor of Commerce"),
        ("SBL1400", "Introduction to Business Analytics", "Bachelor of Commerce"),
        ("SBL1500", "Ethics and Responsibility in Business", "Bachelor of Commerce"),
        ("SBL1800", "Work and Career", "Bachelor of Commerce"),
        ("SBL2800", "Professional Engagement and Planning", "Bachelor of Commerce"),
        ("SBL3800", "Professional Practice", "Bachelor of Commerce"),
        ("MAN1100", "Management", "Bachelor of Commerce"),
        ("MAN2120", "Organisational Behaviour", "Bachelor of Commerce"),
        ("MAN2145", "Human Resource Management", "Bachelor of Commerce"),
        ("SBL2100", "Cultural Business Intelligence", "Bachelor of Commerce"),
        ("MAN2610", "Managing for Sustainability", "Bachelor of Commerce"),
        ("MAN3315", "Negotiation", "Bachelor of Commerce"),
        ("MAN3503", "Strategic Management", "Bachelor of Commerce"),
        ("MAN3121", "Leadership", "Bachelor of Commerce"),
        ("INB2102", "International Business", "Bachelor of Commerce"),
        ("MAN3246", "International Human Resource Management", "Bachelor of Commerce"),
        ("INB3303", "Global Trade", "Bachelor of Commerce"),
        ("INB3202", "International Business Project", "Bachelor of Commerce"),
        ("MKT1600", "Marketing Principles & Practices", "Bachelor of Commerce"),
        ("MKT2810", "Digital Marketing", "Bachelor of Commerce"),
        ("MKT2608", "Understanding Buyer Behaviour", "Bachelor of Commerce"),
        ("MKT2700", "Applied Omnichannel Marketing", "Bachelor of Commerce"),
        ("MKT2805", "Social Media Marketing", "Bachelor of Commerce"),
        ("MKT3600", "Market Analysis and Creative Innovation", "Bachelor of Commerce"),
        ("MKT3820", "Data Analysis for Marketing Insights", "Bachelor of Commerce"),
        ("MKT3601", "Marketing Strategy", "Bachelor of Commerce"),

        // ---- Bachelor of Computer Science ----
        ("SCI1125", "Professional Science Essentials", "Bachelor of Computer Science"),
        ("CSP1150", "Programming Principles", "Bachelor of Computer Science"),
        ("MAT1252", "Mathematics for Computing", "Bachelor of Computer Science"),
        ("CSI1241", "Systems Analysis", "Bachelor of Computer Science"),
        ("CSG1105", "Applied Communications", "Bachelor of Computer Science"),
        ("CSI1101", "Computer Security", "Bachelor of Computer Science"),
        ("ENS1161", "Computer Fundamentals", "Bachelor of Computer Science"),
        ("CSG1207", "Systems and Database Design", "Bachelor of Computer Science"),
        ("CSP2348", "Data Structures", "Bachelor of Computer Science"),
        ("CSP2104", "Object Oriented Programming with C++", "Bachelor of Computer Science"),
        ("CSI2312", "Foundations of Software Engineering", "Bachelor of Computer Science"),
        ("CSI2343", "Object Oriented Analysis and Design", "Bachelor of Computer Science"),
        ("CSG2341", "Artificial Intelligence 1", "Bachelor of Computer Science"),
        ("CSG2344", "Project Methods and Professionalism", "Bachelor of Computer Science"),
        ("CSP2108", "Introduction to Mobile Applications Development", "Bachelor of Computer Science"),
        ("CSI2441", "Applications Development", "Bachelor of Computer Science"),
        ("CSI3344", "Distributed Systems", "Bachelor of Computer Science"),
        ("CSP3341", "Programming Languages and Paradigms", "Bachelor of Computer Science"),
        ("CSI3105", "Software Testing", "Bachelor of Computer Science"),
        ("CSI3106", "Software Architectures and Design", "Bachelor of Computer Science"),
        ("CSG3101", "Applied Project", "Bachelor of Computer Science"),
        ("CSG3309", "IT Security Management", "Bachelor of Computer Science"),
        ("CSI2108", "Cryptographic Concepts", "Bachelor of Computer Science"),
        ("CSG2305", "Digital Forensics", "Bachelor of Computer Science"),
        ("CSI3207", "Network Security", "Bachelor of Computer Science"),
        ("CSI3208", "Ethical Hacking and Defence", "Bachelor of Computer Science"),
        ("MAT3120", "Machine Learning and Data Visualisation", "Bachelor of Computer Science"),

        // ---- Bachelor of Communication ----
        ("CMM1600", "Foundations of Advertising", "Bachelor of Communication"),
        ("CMM1605", "Communication Campaigns", "Bachelor of Communication"),
        ("JBM1600", "Radio and Podcast Production", "Bachelor of Communication"),
        ("CMM1610", "Understanding Audiences", "Bachelor of Communication"),
        ("CMM1615", "Media Content Strategy", "Bachelor of Communication"),
        ("JBM1615", "Media Law and Ethics", "Bachelor of Communication"),
        ("CMM2600", "Digital Content Creation", "Bachelor of Communication"),
        ("CMM2605", "Principles of Inclusive Communication", "Bachelor of Communication"),
        ("JBM2600", "Storytelling and Social Media Influence", "Bachelor of Communication"),
        ("JBM1605", "Fundamentals of Journalism", "Bachelor of Communication"),
        ("CMM2610", "Strategic Branding", "Bachelor of Communication"),
        ("CMM2615", "Strategic Social Media", "Bachelor of Communication"),
        ("CMM3600", "Corporate Communication", "Bachelor of Communication"),
        ("CMM3605", "Global Communication", "Bachelor of Communication"),
        ("CMM3650", "Communication Agency", "Bachelor of Communication"),

        // ---- Bachelor of Science (Cyber Security) — units unique to this course ----
        ("CSI2450", "IoT and OT Security", "Bachelor of Science (Cyber Security)"),
        ("CSP2101", "Scripting Languages", "Bachelor of Science (Cyber Security)"),
        ("CSI2107", "Malware Analysis", "Bachelor of Science (Cyber Security)"),
        ("CSI3350", "Enterprise Security and Governance", "Bachelor of Science (Cyber Security)"),
        ("CSI3351", "Cyber Security Incident Detection and Response", "Bachelor of Science (Cyber Security)"),

        // ---- Bachelor of Design ----
        ("DES1600", "Design Foundations", "Bachelor of Design"),
        ("DES1610", "Digital Design", "Bachelor of Design"),
        ("DES1605", "Design Prototyping", "Bachelor of Design"),
        ("DES1615", "Design Knowledge", "Bachelor of Design"),
        ("DES1620", "Design Thinking", "Bachelor of Design"),
        ("DES2600", "Design Studies", "Bachelor of Design"),
        ("DES2650", "Experience Design Studio", "Bachelor of Design"),
        ("DES2114", "Strategic Visual Communication", "Bachelor of Design"),
        ("DES2660", "Service and System Design Studio", "Bachelor of Design"),
        ("DES3650", "Design Studio 1", "Bachelor of Design"),
        ("DES3660", "Design Studio 2", "Bachelor of Design"),
        ("DES3610", "Design Futures", "Bachelor of Design"),

        // ---- Bachelor of Psychology ----
        ("PSY1101", "Introduction to Psychology", "Bachelor of Psychology"),
        ("PSY1115", "Psychology of Motivation and Emotion", "Bachelor of Psychology"),
        ("PSY1210", "Biopsychology, Sensation and Perception", "Bachelor of Psychology"),
        ("PSY1204", "Social Determinants of Behaviour", "Bachelor of Psychology"),
        ("PSY2102", "Fundamentals of Psychological Inquiry", "Bachelor of Psychology"),
        ("PSY2105", "Psychology of Personality and Individual Differences", "Bachelor of Psychology"),
        ("PSY2204", "Learning, Memory and Cognition", "Bachelor of Psychology"),
        ("PSY2231", "Development Psychology", "Bachelor of Psychology"),
        ("PSY3105", "Advanced Methods of Psychological Inquiry", "Bachelor of Psychology"),
        ("PSY3456", "Mental Health and Psychological Interventions", "Bachelor of Psychology"),
        ("PSY3217", "Cultural Issues in Psychology", "Bachelor of Psychology"),
        ("PSY3225", "Applications of Psychological Literacy", "Bachelor of Psychology"),

        // ---- Bachelor of Technology (Engineering) major in Electrical ----
        ("ENS1154", "Introduction to Engineering", "Bachelor of Technology (Engineering) - Electrical"),
        ("MAT1137", "Introductory Applied Mathematics", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS1115", "Materials and Manufacturing 1", "Bachelor of Technology (Engineering) - Electrical"),
        ("SCP1132", "Introduction to Physics", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS1101", "Engineering Mechanics", "Bachelor of Technology (Engineering) - Electrical"),
        ("MAT1250", "Mathematics 1", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS1253", "Electrical Engineering Fundamentals", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS1180", "Introduction to Energy and Resource Engineering", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS2120", "Engineering Systems", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENM1102", "Engineering Drawing and Computer Aided Design", "Bachelor of Technology (Engineering) - Electrical"),
        ("CSP2151", "Programming Fundamentals", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS2159", "Engineering Innovation and Ethics", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3173", "Technology Practicum", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3201", "Technology Project", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3216", "Engineering Management", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENM2104", "Instrumentation and Measurement", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS2456", "Digital Electronics", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS2457", "Analog Electronics", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3553", "Signals and Systems", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3206", "Power Systems 1", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3212", "Electrical Machines and Transformers", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3551", "Electrical Networks", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS3215", "Power Electronics", "Bachelor of Technology (Engineering) - Electrical"),
        ("ENS5240", "Industrial Control", "Bachelor of Technology (Engineering) - Electrical"),

        // ---- Bachelor of Science (Nursing Studies) ----
        ("NSI1101", "Research Methods, Appraisals and Statistics", "Bachelor of Science (Nursing Studies)"),
        ("NSI1205", "Healthcare Ethics and Law", "Bachelor of Science (Nursing Studies)"),
        ("NSI2303", "Chronic Conditions and Supportive Nursing Care", "Bachelor of Science (Nursing Studies)"),
        ("NSI2402", "Nursing Older People", "Bachelor of Science (Nursing Studies)"),
        ("NSI2307", "Mental Health and Wellness Nursing", "Bachelor of Science (Nursing Studies)"),
        ("NSI3111", "Health Assessment and Clinical Decision Making", "Bachelor of Science (Nursing Studies)"),
        ("NSI3605", "Leadership, Governance and Organisational Culture for Nurses", "Bachelor of Science (Nursing Studies)"),
        ("NSI3613", "Teaching and Learning", "Bachelor of Science (Nursing Studies)"),
    };

    // (StudentId, Name, Course, [(UnitCode, Mark), ...])
    // Every student id starts with "10", matching the ECU student-number
    // format (e.g. 10730907) — the digits after "10" vary per student.
    private static readonly (string, string, string, (string, int)[])[] Students =
        new (string, string, string, (string, int)[])[]
    {
        ("10730907", "Sulakshan Marudanayagam", "Bachelor of Computer Science", new (string, int)[]
        {
            ("CSP3341", 84), ("CSI3344", 76), ("CSI3105", 68), ("CSG3101", 91),
        }),
        ("10812234", "Amelia Chen", "Bachelor of Commerce", new (string, int)[]
        {
            ("SBL1100", 72), ("SBL1200", 65), ("MKT1600", 88), ("MAN1100", 79),
        }),
        ("10845561", "Josiah Nguyen", "Bachelor of Science (Cyber Security)", new (string, int)[]
        {
            ("CSI1101", 90), ("CSI2450", 83), ("CSI3208", 77), ("CSG3309", 71),
        }),
        ("10893012", "Priya Kapoor", "Bachelor of Biomedical Science", new (string, int)[]
        {
            ("MHS1101", 81), ("SCH1133", 74), ("SCH2235", 69), ("SCH3145", 92),
        }),
        ("10711452", "Lachlan Murphy", "Bachelor of Technology (Engineering) - Electrical", new (string, int)[]
        {
            ("ENS1154", 66), ("MAT1250", 58), ("ENS2456", 73), ("ENS3206", 80),
        }),
        ("10768823", "Grace O'Brien", "Bachelor of Design", new (string, int)[]
        {
            ("DES1600", 85), ("DES1610", 78), ("DES2650", 90), ("DES3650", 87),
        }),
        ("10799910", "Ethan Walsh", "Bachelor of Psychology", new (string, int)[]
        {
            ("PSY1101", 62), ("PSY1210", 70), ("PSY2204", 75), ("PSY3456", 68),
        }),
        ("10820456", "Mia Thompson", "Bachelor of Communication", new (string, int)[]
        {
            ("CMM1600", 89), ("JBM1600", 82), ("CMM2600", 94), ("CMM3600", 86),
        }),
        ("10758201", "Noah Fitzgerald", "Bachelor of Science (Nursing Studies)", new (string, int)[]
        {
            ("NSI1101", 71), ("NSI1205", 64), ("NSI2303", 77), ("NSI3111", 80),
        }),
        ("10784567", "Isabella Ricci", "Bachelor of Computer Science", new (string, int)[]
        {
            ("CSP1150", 95), ("CSP2348", 88), ("CSI2312", 91), ("CSP3341", 79),
        }),
    };
}
