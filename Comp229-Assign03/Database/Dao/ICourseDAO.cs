using Comp229_Assign03.Database.Model;
using System.Collections.Generic;

namespace Comp229_Assign03.Database.Dao
{
    interface ICourseDAO : IGenericDAO<Course>
    {
        /// <summary>
        /// Finds all the courses that the given student is not enrolled in.
        /// </summary>
        /// <param name="student">The reference student</param>
        /// <returns>The list of not enrolled courses</returns>
        List<Course> FindAllCoursesNotEnrolledForStudent(Student student);
    }
}
