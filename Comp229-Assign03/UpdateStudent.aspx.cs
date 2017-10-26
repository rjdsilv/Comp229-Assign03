using Comp229_Assign03.Controllers;
using Comp229_Assign03.Database.Exception;
using Comp229_Assign03.Database.Model;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_Assign03
{
    public partial class UpdateStudent : Page
    {
        protected string message = "";
        protected StudentController studentController = StudentController.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Title = "Update Student";

                if (!IsPostBack)
                {
                    bool redirectToStudentManagement = false;

                    if (!string.IsNullOrEmpty(Request.QueryString["student"]))
                    {
                        Student student = studentController.FindStudentById(int.Parse(Request.QueryString["student"]));
                        ViewState["SelectedStudent"] = student;

                        if (!student.IsUnknown())
                        {
                            StudentID.Text = student.Id.ToString();
                            StudentEnrollmentDateTextBox.Text = student.EnrollmentDate;
                            StudentFirstMidNameTextBox.Text = student.FirstMidName;
                            StudentLastNameTextBox.Text = student.LastName;
                        }
                        else
                        {
                            redirectToStudentManagement = true;
                        }
                    }
                    else
                    {
                        redirectToStudentManagement = true;
                    }

                    if (redirectToStudentManagement)
                    {
                        Response.Redirect("StudentManagement");
                    }
                }
            }
            catch (DatabaseException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("The system failed with the following message: " + ex.Message);
            }
        }

        protected void StudentUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = ViewState["SelectedStudent"] as Student;
                student.Id = int.Parse(StudentID.Text);
                student.FirstMidName = StudentFirstMidNameTextBox.Text;
                student.LastName = StudentLastNameTextBox.Text;
                student.EnrollmentDateTime = DateTime.Parse(StudentEnrollmentDateTextBox.Text);
                studentController.UpdateStudent(student);
                ShowSuccessMessage(string.Format("Student {0}, {1} updated successfully on the database.", student.LastName, student.FirstMidName));
            }
            catch (DatabaseException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("The system failed with the following message: " + ex.Message);
            }
        }

        /// <summary>
        /// Shows to the user any unexpecte error that may occur during the database communication.
        /// </summary>
        /// <param name="message">The error message to be shown.</param>
        private void ShowErrorMessage(string message)
        {
            this.message = string.Format("<hr/>The following unexpected error has occurred: <b>{0}</b><hr/>", message);
            ErrorPanel.CssClass = "school-error-message";
        }

        /// <summary>
        /// Shows to the user any unexpecte error that may occur during the database communication.
        /// </summary>
        /// <param name="message">The error message to be shown.</param>
        private void ShowSuccessMessage(string message)
        {
            this.message = string.Format("<hr/>{0}<hr/>", message);
            SuccessUpdatePanel.CssClass = "school-success-message";
        }
    }
}