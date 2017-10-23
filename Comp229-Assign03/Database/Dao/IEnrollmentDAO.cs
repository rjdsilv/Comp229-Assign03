using Comp229_Assign03.Database.Model;
using System.Collections.Generic;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// Interface containing all the usable methods for the EnrollmentDAO object.
    /// </summary>
    public interface IEnrollmentDAO : IGenericDAO<Enrollment>
    {
        /// <summary>
        /// Finds all the enrollments that a given student has.
        /// </summary>
        /// <param name="student">The student to have the enrollments found.</param>
        /// <returns>The list of enrollments for the given student</returns>
        List<Enrollment> FindByStudent(Student student);
    }
}