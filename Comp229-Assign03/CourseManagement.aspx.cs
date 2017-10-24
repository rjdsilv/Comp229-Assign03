using Comp229_Assign03.Controllers;
using Comp229_Assign03.Database.Exception;
using Comp229_Assign03.Database.Model;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_Assign03
{
    public partial class CourseManagement : Page
    {
        // Protect attributes to be used on the page.
        protected string message = "";
        protected Course selectedCourse = new Course();
        protected CourseController courseController = CourseController.GetInstance();
        protected DepartmentController departmentController = DepartmentController.GetInstance();

        // Loads all the data into the page
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Course Management";

            if (!IsPostBack)
            {
                try
                {
                    // Shows all the courses as none was selected
                    if (!string.IsNullOrEmpty(Request.QueryString["course"]))
                    {
                        selectedCourse = courseController.FindCourseById(int.Parse(Request.QueryString["course"]));
                        courseController.GetAllEnrollmentsForCourseAndBindToRepeater(selectedCourse, ref StudentsEnrolledRepeater);

                        if (!string.IsNullOrEmpty(Request.QueryString["unenrolledFirstName"]) && !string.IsNullOrEmpty(Request.QueryString["unenrolledLastName"]))
                        {
                            ShowSuccessMessage(string.Format("Student {0}, {1} successfully unenrolled from the course {2}.", Request.QueryString["unenrolledLastName"], Request.QueryString["unenrolledFirstName"], selectedCourse.Title));
                        }
                    }
                    // Shows the selected course
                    else
                    {
                        departmentController.GetAllDepartmentsAndBindToDropDownList(ref CourseDepartmentDropDown);
                        courseController.GetAllCoursesAndBindToRepeater(ref CoursesRepeater);
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

        // Saves the course to the database.
        protected void CourseSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                courseController.InsertCourse(
                    new Course(
                        0, 
                        CourseTitleTextBox.Text, 
                        int.Parse(CourseCreditTextBox.Text), 
                        new Department(
                            int.Parse(CourseDepartmentDropDown.SelectedItem.Value),
                            CourseDepartmentDropDown.SelectedItem.Text,
                            0.0m
                        )
                    )
                );
                ShowSuccessMessage(courseController.BuildSaveSucessMessage(CourseTitleTextBox.Text));
                ClearTextBoxes();
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

        // Unenrolls the student from the given course.
        protected void RemoveEnrollmentImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton unenrollStudentImageButton = sender as ImageButton;
            Enrollment enrollment = courseController.FindEnrollmentById(int.Parse(unenrollStudentImageButton.CommandArgument));
            courseController.DeleteEnrollment(enrollment);
            Response.Redirect(string.Format("CourseManagement?course={0}&unenrolledFirstName={1}&unenrolledLastName={2}", Request.QueryString["course"], enrollment.Student.FirstMidName, enrollment.Student.LastName));
        }

        /// <summary>
        /// Clears the text boxes from the students inclusion panel.
        /// </summary>
        private void ClearTextBoxes()
        {
            CourseCreditTextBox.Text = "";
            CourseTitleTextBox.Text = "";
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