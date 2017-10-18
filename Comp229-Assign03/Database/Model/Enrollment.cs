namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Enrollment
    /// <b>Description</b>: Model class to hold Enrollment data from the database.
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class Enrollment : GenericModel
    {
        public Course course { get; set; }
        public Student student { get; set; }
        public int grade { get; set; }

        /// <summary>
        /// Creates a new instance of Enrollment class.
        /// </summary>
        /// <param name="enrollmentID"></param> - The enrollment's identification.
        /// <param name="course"></param> - The enrollment's course.
        /// <param name="student"></param> - The enrollment's student.
        /// <param name="grade"></param> - The enrollment's grade.
        public Enrollment(int enrollmentID, Course course, Student student, int grade)
        {
            id = enrollmentID;
            this.course = course;
            this.student = student;
            this.grade = grade;
        }
    }
}