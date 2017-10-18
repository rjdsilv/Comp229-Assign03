namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Course
    /// <b>Description</b>: Model class to hold Course data from the database.
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class Course : GenericModel
    {
        public string title { get; set; }
        public int credits { get; set; }
        public Department department { get; set; }

        /// <summary>
        /// Creates a new instance of Course class.
        /// </summary>
        /// <param name="courseID"></param> - The course's identification.
        /// <param name="title"></param> - The course's title
        /// <param name="credits"></param> - The course's number of credits.
        /// <param name="department"></param> - The department to which the course belongs to.
        public Course(int courseID, string title, int credits, Department department)
        {
            id = courseID;
            this.title = title;
            this.credits = credits;
            this.department = department;
        }
    }
}