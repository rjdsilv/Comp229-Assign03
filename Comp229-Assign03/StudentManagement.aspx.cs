using Comp229_Assign03.Controllers;
using Comp229_Assign03.Database.Exception;
using Comp229_Assign03.Database.Model;
using System;
using System.Web.UI;

namespace Comp229_Assign03
{
    public partial class StudentManagement : Page
    {
        // Protect attributes to be used on the page.
        protected StudentController studentController = StudentController.GetInstance();
        protected string errorMessage = "";


        // Raised when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Student Management";

            if (!IsPostBack)
            {
                try
                {
                    // Shows all the students as none was selected
                    if (string.IsNullOrEmpty(Request.QueryString["student"]))
                    {
                        studentController.GetAllStudentsAndBindToRepeater(ref StudentsRepeater);
                    }
                    // Shows the selected student
                    else
                    {

                    }
                }
                catch (DatabaseException ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        // Saves the students to the database.
        protected void StudentSaveButton_Click(object sender, EventArgs e)
        {
            studentController.InsertStudent(new Student(0, StudentLastNameTextBox.Text, StudentFirstMidNameTextBox.Text, DateTime.Now));
            studentController.GetAllStudentsAndBindToRepeater(ref StudentsRepeater);
            Response.Write(studentController.BuildSaveSucessMessage(StudentFirstMidNameTextBox.Text, StudentLastNameTextBox.Text));
            ClearTextBoxes();
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
        /// Shows to the user any unexpecte error that may occur during the database communication.
        /// </summary>
        /// <param name="message">The error message to be shown.</param>
        private void ShowErrorMessage(string message)
        {
            errorMessage = string.Format("<hr/>The following unexpected error has occurred: <b>{0}</b><hr/>", message);
            ErrorPanel.CssClass = "school-error-message";
        }
    }
}