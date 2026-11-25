using StudentManagementApi.Models;

namespace StudentManagementApi.Services
{
    /// <summary>
    /// Thread-safe in-memory implementation of IStudentService.
    /// Pre-seeded with sample student records for immediate API testing.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new();
        private readonly object _lock = new();
        private int _nextId = 1;

        public StudentService()
        {
            // Seed initial student records
            Create(new Student
            {
                EnrollmentNo = "92400120001",
                FullName = "Aarav Patel",
                Department = "Computer Science and Engineering",
                Semester = 5,
                Email = "aarav.patel@example.edu",
                Cgpa = 8.45
            });

            Create(new Student
            {
                EnrollmentNo = "92400120002",
                FullName = "Priya Sharma",
                Department = "Information Technology",
                Semester = 5,
                Email = "priya.sharma@example.edu",
                Cgpa = 9.10
            });

            Create(new Student
            {
                EnrollmentNo = "92400120003",
                FullName = "Rohan Mehta",
                Department = "Computer Science and Engineering",
                Semester = 3,
                Email = "rohan.mehta@example.edu",
                Cgpa = 7.85
            });
        }

        public IEnumerable<Student> GetAll(string? department = null)
        {
            lock (_lock)
            {
                IEnumerable<Student> query = _students;

                if (!string.IsNullOrWhiteSpace(department))
                {
                    query = query.Where(s =>
                        s.Department.Contains(department, StringComparison.OrdinalIgnoreCase));
                }

                return query.ToList();
            }
        }

        public Student? GetById(int id)
        {
            lock (_lock)
            {
                return _students.FirstOrDefault(s => s.Id == id);
            }
        }

        public Student Create(Student student)
        {
            lock (_lock)
            {
                student.Id = _nextId++;
                _students.Add(student);
                return student;
            }
        }

        public Student? Update(int id, Student updatedStudent)
        {
            lock (_lock)
            {
                var existing = _students.FirstOrDefault(s => s.Id == id);
                if (existing is null)
                {
                    return null;
                }

                existing.EnrollmentNo = updatedStudent.EnrollmentNo;
                existing.FullName = updatedStudent.FullName;
                existing.Department = updatedStudent.Department;
                existing.Semester = updatedStudent.Semester;
                existing.Email = updatedStudent.Email;
                existing.Cgpa = updatedStudent.Cgpa;

                return existing;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                var existing = _students.FirstOrDefault(s => s.Id == id);
                if (existing is null)
                {
                    return false;
                }

                _students.Remove(existing);
                return true;
            }
        }
    }
}
