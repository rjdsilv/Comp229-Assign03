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

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Home Page";

            if (!IsPostBack)
            {
                // Loads the all the students from the database.
                List<Student> allStudents = studentDAO.FindAll();
                studentsRepeater.DataSource = allStudents;
                studentsRepeater.DataBind();
            }
        }
    }
}