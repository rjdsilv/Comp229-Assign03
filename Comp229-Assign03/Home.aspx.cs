using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_Assign03
{
    public partial class _Default : Page
    {
        private StudentDAO studentDAO = StudentDAO.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Student> allStudents = studentDAO.FindAll();
                studentsRepeater.DataSource = allStudents;
                studentsRepeater.DataBind();
            }
        }

        protected void SelectStudent(string rowId)
        {
            System.Diagnostics.Debug.WriteLine("Row Clicked: " + rowId);
        }
    }
}