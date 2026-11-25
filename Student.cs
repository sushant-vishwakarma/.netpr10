using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.Models
{
    /// <summary>
    /// Represents a student entity managed by the REST API.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Unique identifier assigned to the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// University enrollment number of the student.
        /// </summary>
        [Required(ErrorMessage = "Enrollment number is required.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Enrollment number must be between 4 and 20 characters.")]
        public string EnrollmentNo { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the student.
        /// </summary>
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Academic department (e.g., Computer Science, Information Technology).
        /// </summary>
        [Required(ErrorMessage = "Department is required.")]
        [StringLength(80)]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Current academic semester (1 to 8).
        /// </summary>
        [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
        public int Semester { get; set; }

        /// <summary>
        /// Institutional or personal email address.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Cumulative Grade Point Average (0.0 to 10.0).
        /// </summary>
        [Range(0.0, 10.0, ErrorMessage = "CGPA must be between 0.0 and 10.0.")]
        public double Cgpa { get; set; }
    }
}
