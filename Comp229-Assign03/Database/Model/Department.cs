using System;

namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Department
    /// <b>Description</b>: Model class to hold Department data from the database.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    [Serializable]
    public class Department : GenericModel
    {
        public string Name { get; set; }
        public decimal Budget { get; set; }

        /// <summary>
        /// Creates a new unknown instance of the Department class.
        /// </summary>
        internal Department() : base()
        {
            Name = "UNKNOWN";
            Budget = 0.0m;
        }

        /// <summary>
        /// Creates a new instance of Department class.
        /// </summary>
        /// <param name="departmentID">The department's identification.</param>
        /// <param name="name">The department's name.</param>
        /// <param name="budget">The department's budget.</param>
        public Department(int departmentID, string name, decimal budget)
        {
            Id = departmentID;
            Name = name;
            Budget = budget;
        }
    }
}