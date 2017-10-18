﻿using System;

namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : Student
    /// <b>Description</b>: Model class to hold Student data from the database.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class Student : GenericModel
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Creates a new unknown instance of the Student class.
        /// </summary>
        internal Student() : base()
        {
            LastName = "UNKNOWN";
            FirstMidName = "UNKNOWN";
            EnrollmentDate = DateTime.MinValue;
        }

        /// <summary>
        /// Creates a new instance of Student class.
        /// </summary>
        /// <param name="studentID">The student's identification.</param>
        /// <param name="lastName">The student's last name.</param>
        /// <param name="firstMidName">The student's firt and middle names.</param>
        /// <param name="erollmentDate">The student's enrollment date.</param>
        public Student(int studentID, string lastName, string firstMidName, DateTime enrollmentDate)
        {
            Id = studentID;
            this.LastName = lastName;
            this.FirstMidName = firstMidName;
            this.EnrollmentDate = enrollmentDate;
        }
    }
}