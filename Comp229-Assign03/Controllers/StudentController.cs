using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Patterns;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Comp229_Assign03.Controllers
{
    public class StudentController : Singleton<StudentController>
    {
        private StudentDAO studentDAO = StudentDAO.GetInstance();

        private StudentController()
        { }

        public void InsertStudent(Student student)
        {
            studentDAO.Insert(student);
        }

        /// <summary>
        /// Gets all the students from the database and bind it into the Repeater.
        /// <paramref name="repeater">The repeater to be bound.</param>
        /// </summary>
        public void GetAllStudentsAndBindToRepeater(ref Repeater repeater)
        {
            List<Student> allStudents = studentDAO.FindAll();
            repeater.DataSource = allStudents;
            repeater.DataBind();
        }

        /// <summary>
        /// Builds the success message for successfully included students.
        /// </summary>
        /// <returns>The message built</returns>
        public string BuildSaveSucessMessage(string firstName, string lastName)
        {
            string message = string.Format("<script type='text/javascript'>alert('Student {0}, {1} successfully saved on the database!');</script>", lastName, firstName);
            return message;
        }

        public Student FindStudentById(int id)
        {
            return studentDAO.FindById(id);
        }
    }
}