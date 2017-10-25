using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Patterns;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Comp229_Assign03.Controllers
{
    /// <summary>
    /// <b>Class</b>      : StudentController
    /// <b>Description</b>: Controller class for dealing with Students and its operations.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class StudentController : Singleton<StudentController>
    {
        // Private attributes declaration.
        private IStudentDAO studentDAO = StudentDAO.GetInstance();
        private IEnrollmentDAO enrollmentDAO = EnrollmentDAO.GetInstance();
        private ICourseDAO courseDAO = CourseDAO.GetInstance();

        /// <summary>
        /// Creates a new instance of the StudentController class.
        /// </summary>
        private StudentController()
        { }

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
        /// Finds a course by its given id.
        /// </summary>
        /// <param name="id">The course identification on the database.</param>
        /// <returns>The course found</returns>
        public Course FindCourseById(int id)
        {
            return courseDAO.FindById(id);
        }

        /// <summary>
        /// Inserts the given student on the database.
        /// </summary>
        /// <param name="student">The student to be inserted.</param>
        public void InsertStudent(Student student)
        {
            studentDAO.Insert(student);
        }

        /// <summary>
        /// Inserts the given enrollment on the database.
        /// </summary>
        /// <param name="enrollment">The course to be inserted.</param>
        public void InsertEnrollment(Enrollment enrollment)
        {
            enrollmentDAO.Insert(enrollment);
        }

        /// <summary>
        /// This method will delete the given student and all its dependencies.
        /// </summary>
        /// <param name="student">The student to be deleted.</param>
        public void DeleteStudentAndDependencies(Student student)
        {
            studentDAO.DeleteStudentAndDependencies(student);
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
        /// Gets all the students from the database and bind it into the Repeater.
        /// <paramref name="repeater">The repeater to be bound.</param>
        /// </summary>
        public void GetAllStudentsAndBindToRepeater(ref Repeater repeater)
        {
            repeater.DataSource = studentDAO.FindAll();
            repeater.DataBind();
        }

        /// <summary>
        /// Finds all enrolled courses for a given student and binds it to the given repeater.
        /// </summary>
        /// <param name="student">The student whose erolled courses must be found</param>
        /// <return>The number of enrolled courses.</return>
        public int GetAllEnrollmentsForStudentAndBindToRepeater(Student student, ref Repeater repeater)
        {
            List<Enrollment> allEnrollments = enrollmentDAO.FindByStudent(student);
            repeater.DataSource = allEnrollments;
            repeater.DataBind();

            return allEnrollments.Count;
        }

        /// <summary>
        /// Finds all the students not enrolled in the given course and binds it to the repeater.
        /// </summary>
        /// <param name="student">The reference course</param>
        /// <param name="repeater">The repeater to be bound</param>
        /// <returns>The number of enrollments</returns>
        public int GetAllNotEnrolledCoursesForStudentAndBindToRepeater(Student student, ref Repeater repeater)
        {
            List<Course> notEnrolledCourses = courseDAO.FindAllCoursesNotEnrolledForStudent(student);
            repeater.DataSource = notEnrolledCourses;
            repeater.DataBind();

            return notEnrolledCourses.Count;
        }

        /// <summary>
        /// Builds the success message for successfully included students.
        /// </summary>
        /// <param name="firstName">The student's first name</param>
        /// <param name="lastName">The student's last name</param>
        /// <returns>The message built</returns>
        public string BuildSaveSucessMessage(string firstName, string lastName)
        {
            return string.Format("Student {0}, {1} successfully saved on the database!", lastName, firstName);
        }

        /// <summary>
        /// Builds the successful student removal message for successfully removed students.
        /// </summary>
        /// <param name="firstName">The student's first name</param>
        /// <param name="lastName">The student's last name</param>
        /// <returns>The message built</returns>
        public string BuildSucessfulRemovalMessage(string firstName, string lastName)
        {
            return string.Format("The student {0}, {1} was successfully removed from the database.", lastName, firstName);
        }

        /// <summary>
        /// Finds a student by its given id.
        /// </summary>
        /// <param name="id">The student identification on the database.</param>
        /// <returns>The student found</returns>
        public Student FindStudentById(int id)
        {
            return studentDAO.FindById(id);
        }
    }
}