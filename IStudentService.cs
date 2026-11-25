using StudentManagementApi.Models;

namespace StudentManagementApi.Services
{
    /// <summary>
    /// Defines CRUD operations for managing student records.
    /// </summary>
    public interface IStudentService
    {
        IEnumerable<Student> GetAll(string? department = null);
        Student? GetById(int id);
        Student Create(Student student);
        Student? Update(int id, Student updatedStudent);
        bool Delete(int id);
    }
}
