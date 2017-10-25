using Comp229_Assign03.Controllers;
using Comp229_Assign03.Database.Exception;
using Comp229_Assign03.Database.Model;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_Assign03
{
    public partial class StudentManagement : Page
    {
        // Protect attributes to be used on the page.
        protected StudentController studentController = StudentController.GetInstance();
        protected Student selectedStudent = new Student();
        protected string message = "";
        protected int enrolledCourses = 0;
        protected int notEnrolledCourses = 0;


        // Raised when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Student Management";

            if (!IsPostBack)
            {
                try
                {
                    // Shows all the students as none was selected
                    if (!string.IsNullOrEmpty(Request.QueryString["student"]))
                    {
                        selectedStudent = studentController.FindStudentById(int.Parse(Request.QueryString["student"]));
                        enrolledCourses = studentController.GetAllEnrollmentsForStudentAndBindToRepeater(selectedStudent, ref StudentEnrolledCoursesRepeater);
                        notEnrolledCourses = studentController.GetAllNotEnrolledCoursesForStudentAndBindToRepeater(selectedStudent, ref StudentNotEnrolledCoursesRepeater);
                        ViewState["SelectedStudent"] = selectedStudent;

                        if (!string.IsNullOrEmpty(Request.QueryString["unenrolledCourse"]))
                        {
                            ShowSuccessMessage(string.Format("Student {0}, {1} successfully unenrolled from the {2} course.", selectedStudent.LastName, selectedStudent.FirstMidName, Request.QueryString["unenrolledCourse"]));
                        }
                        else if (!string.IsNullOrEmpty(Request.QueryString["enrolledCourse"]))
                        {
                            ShowSuccessMessage(string.Format("Student {0}, {1} successfully enrolled in the {2} course.", selectedStudent.LastName, selectedStudent.FirstMidName, Request.QueryString["enrolledCourse"]));
                        }
                    }
                    // Shows the selected student
                    else
                    {
                        studentController.GetAllStudentsAndBindToRepeater(ref StudentsRepeater);
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
        }

        // Saves the students to the database.
        protected void StudentSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                studentController.InsertStudent(new Student(0, StudentLastNameTextBox.Text, StudentFirstMidNameTextBox.Text, DateTime.Now));
                studentController.GetAllStudentsAndBindToRepeater(ref StudentsRepeater);
                ShowSuccessMessage(studentController.BuildSaveSucessMessage(StudentFirstMidNameTextBox.Text, StudentLastNameTextBox.Text));
                ClearTextBoxes();
            }
            catch (DatabaseException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage("The system failed with the following message: " + ex.Message);
            }
        }

        // Removes the selected student and all the enrolled courses he/she has.
        protected void RemoveStudentImageButton_Click(object sender, ImageClickEventArgs e)
        {
            selectedStudent = ViewState["SelectedStudent"] as Student;
            studentController.DeleteStudentAndDependencies(selectedStudent);
            Response.Redirect(string.Format("~/Home?removedFirstName={0}&removedLastName={1}", selectedStudent.FirstMidName, selectedStudent.LastName));
        }

        // Unenrolls the student from the given course.
        protected void RemoveEnrollmentImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton unenrollStudentImageButton = sender as ImageButton;
            Enrollment enrollment = studentController.FindEnrollmentById(int.Parse(unenrollStudentImageButton.CommandArgument));
            studentController.DeleteEnrollment(enrollment);
            Response.Redirect(string.Format("StudentManagement?student={0}&unenrolledCourse={1}", Request.QueryString["student"], enrollment.Course.Title));
        }

        protected void CourseEnrollmentImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton enrollStudentImageButton = sender as ImageButton;
            Course course = studentController.FindCourseById(int.Parse(enrollStudentImageButton.CommandArgument));
            studentController.InsertEnrollment(new Enrollment(0,  course, ViewState["SelectedStudent"] as Student, 0));
            Response.Redirect(string.Format("StudentManagement?student={0}&enrolledCourse={1}", Request.QueryString["student"], course.Title));
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
            SuccessRemovalPanel.CssClass = "school-success-message";
        }
    }
}