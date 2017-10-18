namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Department
    /// <b>Description</b>: Model class to hold Department data from the database.
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class Department : GenericModel
    {
        public string name { get; set; }
        public float budget { get; set; }

        /// <summary>
        /// Creates a new instance of Department class.
        /// </summary>
        /// <param name="departmentID"></param> - The department's identification.
        /// <param name="name"></param> - The department's name.
        /// <param name="budget"></param> - The department's budget.
        public Department(int departmentID, string name, float budget)
        {
            id = departmentID;
            this.name = name;
            this.budget = budget;
        }
    }
}