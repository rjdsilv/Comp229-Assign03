using Comp229_Assign03.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// Interface containing all the usable methods for the EnrollmentDAO object.
    /// </summary>
    public interface IStudentDAO : IGenericDAO<Student>
    {
        /// <summary>
        /// Deletes an student and all its dependencies.
        /// </summary>
        /// <param name="student">The stdent to be deleted</param>
        void DeleteStudentAndDependencies(Student student);

        /// <summary>
        /// Finds all students that are not enrolled on the given course.
        /// </summary>
        /// <param name="course">The course which students are not enrolled.</param>
        /// <returns>The list of students not enrolled in the given course</returns>
        List<Student> FindAllStudentsNotEnrolledInCourse(Course course);
    }
}