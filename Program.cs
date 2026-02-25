using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment2
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public int Grade { get; set; }

        public Student(int id, string name, string course, int grade)
        {
            StudentID = id;
            Name = name;
            Course = course;
            Grade = grade;
        }

        public override string ToString() => $"{StudentID},{Name},{Course},{Grade}";
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "students.txt";

            // Task 1
            var students = new List<Student>
            {
                new Student(2444, "Vince", "BSIT", 89),
                new Student(1335, "Kristel", "BSIT", 92),
                new Student(3557, "Boots", "BSIT", 75),
                new Student(6889, "Dora", "BSIT", 85),
                new Student(9002, "Jaylito", "BSIT", 90)
            };

            File.WriteAllLines(filePath, students.Select(s => s.ToString()));
            var full = Path.GetFullPath(filePath);
            Console.WriteLine($"File '{filePath}' created and written ({students.Count} records).\nFull path: {full}");
            if (File.Exists(filePath))
            {
                Console.WriteLine("\nFile preview:");
                foreach (var line in File.ReadAllLines(filePath))
                    Console.WriteLine(line);
                Console.WriteLine();
            }

            // Task 2
            var lines = File.ReadAllLines(filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();

            var loaded = lines.Select(line =>
            {
                var parts = line.Split(',');
                return new Student(
                    int.Parse(parts[0].Trim()),
                    parts[1].Trim(),
                    parts.Length > 2 ? parts[2].Trim() : string.Empty,
                    parts.Length > 3 ? int.Parse(parts[3].Trim()) : 0);
            }).ToList();

            // LINQ method syntax queries
            Console.WriteLine("Students with Grade > 85");
            var high = loaded.Where(s => s.Grade > 85);
            foreach (var s in high)
                Console.WriteLine($"{s.Name} - {s.Grade}");

            Console.WriteLine("\nSorted by Grade (Descending)");
            var sorted = loaded.OrderByDescending(s => s.Grade);
            foreach (var s in sorted)
                Console.WriteLine($"{s.Name} - {s.Grade}");

            Console.WriteLine("\nStudent Names:");
            var names = loaded.Select(s => s.Name);
            foreach (var n in names)
                Console.WriteLine(n);

            var avg = loaded.Any() ? loaded.Average(s => s.Grade) : 0.0;
            Console.WriteLine($"\nAverage Grade: {avg:F2}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
