using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Patterns;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Comp229_Assign03.Controllers
{
    /// <summary>
    /// <b>Class</b>      : CourseController
    /// <b>Description</b>: Controller class for dealing with Courses and its operations.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class CourseController : Singleton<CourseController>
    {
        // Private attributes declaration.
        private ICourseDAO courseDAO = CourseDAO.GetInstance();
        private IEnrollmentDAO enrollmentDAO = EnrollmentDAO.GetInstance();

        /// <summary>
        /// Creates a new instance of the CourseController class.
        /// </summary>
        private CourseController()
        { }

        /// <summary>
        /// Finds a course by its given id.
        /// </summary>
        /// <param name="id">The course identification on the database.</param>
        /// <returns>The course found</returns>
        public Course FindCourseById(int id)
        {
            return courseDAO.FindById(id);
        }

        /// <summary>
        /// Finds an erollment by its given id.
        /// </summary>
        /// <param name="id">The enrollment identification on the database.</param>
        /// <returns>The enrollment found</returns>
        public Enrollment FindEnrollmentById(int id)
        {
            return enrollmentDAO.FindById(id);
        }

        /// <summary>
        /// Inserts the given course on the database.
        /// </summary>
        /// <param name="course">The course to be inserted.</param>
        public void InsertCourse(Course course)
        {
            courseDAO.Insert(course);
        }

        /// <summary>
        /// Deletes from the database the given enrollment.
        /// </summary>
        /// <param name="enrollment">The enrollment's id to be deleted</param>
        public void DeleteEnrollment(Enrollment enrollment)
        {
            enrollmentDAO.Delete(enrollment);
        }

        /// <summary>
        /// Gets all the courses from the database and bind it into the Repeater.
        /// <paramref name="repeater">The repeater to be bound.</param>
        /// </summary>
        public void GetAllCoursesAndBindToRepeater(ref Repeater repeater)
        {
            repeater.DataSource = courseDAO.FindAll();
            repeater.DataBind();
        }

        /// <summary>
        /// Finds all enrolled students for a given course and binds it to the given repeater.
        /// </summary>
        /// <param name="course">The student whose erolled courses must be found</param>
        /// <return>The number of enrolled courses.</return>
        public int GetAllEnrollmentsForCourseAndBindToRepeater(Course course, ref Repeater repeater)
        {
            List<Enrollment> allEnrollments = enrollmentDAO.FindByCourse(course);
            repeater.DataSource = allEnrollments;
            repeater.DataBind();

            return allEnrollments.Count;
        }

        /// <summary>
        /// Builds the success message for successfully included courses.
        /// </summary>
        /// <param name="title">The course title</param>
        /// <returns>The message built</returns>
        public string BuildSaveSucessMessage(string title)
        {
            return string.Format("Course {0} successfully saved on the database!", title);
        }
    }
}
