using Comp229_Assign03.Database.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        /// <summary>
        /// Deletes all the enrollments for a given student transactionally.
        /// </summary>
        /// <param name="cnn">The connection to be used.</param>
        /// <param name="tran">The transaction to be used.</param>
        /// <param name="student">The student whose enrollments should be deleted.</param>
        void DeleteForStudent(SqlConnection cnn, SqlTransaction tran, Student student);
    }
}