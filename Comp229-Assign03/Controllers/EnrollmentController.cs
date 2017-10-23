using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Patterns;
using System.Collections.Generic;

namespace Comp229_Assign03.Controllers
{
    /// <summary>
    /// <b>Class</b>      : EnrollmentController
    /// <b>Description</b>: Controller class for dealing with Enrollments and its operations.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class EnrollmentController : Singleton<EnrollmentController>
    {
        // Private attributes declaration.
        private IEnrollmentDAO enrollmentDAO = EnrollmentDAO.GetInstance();

        /// <summary>
        /// Finds all enrolled courses for a given student.
        /// </summary>
        /// <param name="student">The student whose erolled courses must be found</param>
        /// <return>The list of enrolled courses for the given student.</return>
        public List<Enrollment> FindAllForStudent(Student student)
        {
            return enrollmentDAO.FindByStudent(student);
        }
    }
}