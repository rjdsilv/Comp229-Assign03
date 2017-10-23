using System;

namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Course
    /// <b>Description</b>: Model class to hold Course data from the database.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    [Serializable]
    public class Course : GenericModel
    {
        public string Title { get; set; }
        public int Credits { get; set; }
        public Department Department { get; set; }

        /// <summary>
        /// Creates a new unknown instance of the Course class.
        /// </summary>
        internal Course() : base()
        {
            Title = "UNKNOWN";
            Credits = 0;
            Department = new Department();
        }

        /// <summary>
        /// Creates a new instance of Course class.
        /// </summary>
        /// <param name="courseID">The course's identification.</param>
        /// <param name="title">The course's title.</param>
        /// <param name="credits">The course's number of credits.</param>
        /// <param name="department">The department to which the course belongs to.</param>
        public Course(int courseID, string title, int credits, Department department)
        {
            Id = courseID;
            this.Title = title;
            this.Credits = credits;
            this.Department = department;
        }

        /// <summary>
        /// Converts the course to string format.
        /// </summary>
        /// <returns>The stringfied course</returns>
        public override string ToString()
        {
            return "Course: {Id = " + Id + ", Title = " + Title + "}";
        }
    }
}