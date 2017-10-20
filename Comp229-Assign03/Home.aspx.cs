using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Database.Model;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Comp229_Assign03
{
    public partial class _Default : Page
    {
        private StudentDAO studentDAO = StudentDAO.GetInstance();

        // Runs when the page is loading.
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Home Page";

            if (!IsPostBack)
            {
                GetAllStudentsAndBindToRepeater();
            }
        }

        // Saves the students to the database.
        protected void StudentSaveButton_Click(object sender, EventArgs e)
        {
            studentDAO.Insert(new Student(0, StudentLastNameTextBox.Text, StudentFirstMidNameTextBox.Text, DateTime.Now));
            GetAllStudentsAndBindToRepeater();
            Response.Write(BuildSucessMessage());

        }

        /// <summary>
        /// Gets all the students from the database and bind it into the Repeater.
        /// </summary>
        private void GetAllStudentsAndBindToRepeater()
        {
            List<Student> allStudents = studentDAO.FindAll();
            StudentsRepeater.DataSource = allStudents;
            StudentsRepeater.DataBind();
        }

        /// <summary>
        /// Clears the text boxes from the students inclusion panel.
        /// </summary>
        private void ClearTextBoxes()
        {
            StudentLastNameTextBox.Text = "";
            StudentFirstMidNameTextBox.Text = "";
        }

        /// <summary>
        /// Builds the success message for successfully included students.
        /// </summary>
        /// <returns>The message built</returns>
        private string BuildSucessMessage()
        {
            string message = string.Format("<script type='text/javascript'>alert('Student {0}, {1} successfully saved on the database!');</script>", StudentLastNameTextBox.Text, StudentFirstMidNameTextBox.Text);
            ClearTextBoxes();
            return message;
        }
    }
}