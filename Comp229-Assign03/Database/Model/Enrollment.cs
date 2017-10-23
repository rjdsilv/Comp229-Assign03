using System;

namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Enrollment
    /// <b>Description</b>: Model class to hold Enrollment data from the database.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    [Serializable]
    public class Enrollment : GenericModel
    {
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int Grade { get; set; }

        /// <summary>
        /// Creates a new unknown instance of the Enrollment class.
        /// </summary>
        internal Enrollment() : base()
        {
            Course = new Course();
            Student = new Student();
            Grade = 0;
        }

        /// <summary>
        /// Creates a new instance of Enrollment class.
        /// </summary>
        /// <param name="enrollmentID">The enrollment's identification.</param>
        /// <param name="course">The enrollment's course.</param>
        /// <param name="student">The enrollment's student.</param>
        /// <param name="grade">The enrollment's grade.</param>
        public Enrollment(int enrollmentID, Course course, Student student, int grade)
        {
            Id = enrollmentID;
            this.Course = course;
            this.Student = student;
            this.Grade = grade;
        }
    }
}