using Comp229_Assign03.Database.Dao;
using Comp229_Assign03.Patterns;
using System.Web.UI.WebControls;

namespace Comp229_Assign03.Controllers
{
    /// <summary>
    /// <b>Class</b>      : DepartmentController
    /// <b>Description</b>: Controller class for dealing with Departments and its operations.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class DepartmentController : Singleton<DepartmentController>
    {
        // Private attributes declaration.
        private IDepartmentDAO departmentDAO = DepartmentDAO.GetInstance();

        /// <summary>
        /// Creates a new instance of the DepartmentController class.
        /// </summary>
        private DepartmentController()
        { }

        /// <summary>
        /// Gets all the departments from the database and bind it into the DropDownList.
        /// <paramref name="dropDownList">The drop down list to be bound.</param>
        /// </summary>
        public void GetAllDepartmentsAndBindToDropDownList(ref DropDownList dropDownList)
        {
            dropDownList.DataTextField = "Name";
            dropDownList.DataValueField = "Id";
            dropDownList.DataSource = departmentDAO.FindAll();
            dropDownList.DataBind();
        }
    }
}