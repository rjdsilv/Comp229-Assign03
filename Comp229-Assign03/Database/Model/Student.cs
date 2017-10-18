using System;

namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Student
    /// <b>Description</b>: Model class to hold Student data from the database.
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class Student : GenericModel
    {
        public string lastName { get; set; }
        public string firstMidName { get; set; }
        public DateTime enrollmentDate { get; set; }

        /// <summary>
        /// Creates a new instance of Student class.
        /// </summary>
        /// <param name="studentID"></param> - The student's identification.
        /// <param name="lastName"></param> - The student's last name
        /// <param name="firstMidName"></param> - The student's firt and middle names.
        /// <param name="erollmentDate"></param> - The student's enrollment date.
        public Student(int studentID, string lastName, string firstMidName, DateTime enrollmentDate)
        {
            id = studentID;
            this.lastName = lastName;
            this.firstMidName = firstMidName;
            this.enrollmentDate = enrollmentDate;
        }
    }
}