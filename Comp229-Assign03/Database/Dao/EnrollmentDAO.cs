using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Database.Exception;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : EnrollmentDAO
    /// <b>Description</b>: DAO class for dealing with database operations regarding the Enrollments table.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class EnrollmentDAO : GenericDAO<Enrollment, EnrollmentDAO>, IEnrollmentDAO
    {
        /// <summary>
        /// Creates a new instance of the EnrollmentDAO class.
        /// </summary>
        protected EnrollmentDAO()
        {
            modelName = "Enrollment";
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildFindAllCommand(SqlConnection cnn)
        {
            return new SqlCommand(BuildCompleteSelectAndFromClauses(), cnn);
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildFindByIdCommand(SqlConnection cnn, int id)
        {
            string cmdText = BuildCompleteSelectAndFromClauses() + " where en.EnrollmentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, id);

            return cmd;
        }

        ///
        /// <see cref="IEnrollmentDAO"></see> 
        ///
        public List<Enrollment> FindByStudent(Student student)
        {
            List<Enrollment> allEnrollments = new List<Enrollment>();

            try
            {
                // Disposes and closes automatically the connection when exiting the using statement.
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    // Disposes automatically the command when exiting the using statement.
                    string cmdText = BuildCompleteSelectAndFromClauses() + " where en.StudentID = @StudentID order by CourseID";
                    using (SqlCommand cmd = new SqlCommand(cmdText, cnn))
                    {
                        AddCommandParameter(cmd, "@StudentID", student.Id);
                        cnn.Open();

                        // Disposes and closes automatically the data reader when exiting the using statement.
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    allEnrollments.Add(BuildObjectFromReader(reader));
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(string.Format("An error has occurred when getting all the {0}s for the Student {1} from the database! Please check if your database is online and set up correctly.", modelName, student.Id), ex);
            }

            return allEnrollments;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildDeleteCommand(SqlConnection cnn, Enrollment modelObject)
        {
            string cmdText = "delete from Enrollments where EnrollmentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildInsertCommand(SqlConnection cnn, Enrollment modelObject)
        {
            string cmdText = "insert into Enrollments(Grade, CourseID, StudentID) values(@Grade, @CourseID, @StudentID)";
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@Grade", modelObject.Grade);
            AddCommandParameter(cmd, "@CourseID", modelObject.Course.Id);
            AddCommandParameter(cmd, "@StudentID", modelObject.Student.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildUpdateCommand(SqlConnection cnn, Enrollment modelObject)
        {
            string cmdText = "update Enrollments set Grade = @Grade where EnrollmentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@Grade", modelObject.Grade);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override Enrollment BuildObjectFromReader(SqlDataReader reader)
        {
            return new Enrollment(
                reader.GetInt32(reader.GetOrdinal("EnrollmentID")),         // The Student's enrollment id.
                new Course(
                    reader.GetInt32(reader.GetOrdinal("CourseID")),         // The Course's id.
                    reader.GetString(reader.GetOrdinal("Title")),           // The Course's title.
                    reader.GetInt32(reader.GetOrdinal("Credits")),          // The Course's credits.
                    new Department(
                        reader.GetInt32(reader.GetOrdinal("DepartmentID")), // The Department's id.
                        reader.GetString(reader.GetOrdinal("Name")),        // The Department's name.
                        reader.GetDecimal(reader.GetOrdinal("Budget"))      // The Department's budget.
                    )
                ),
                new Student(
                    reader.GetInt32(reader.GetOrdinal("StudentID")),        // The Student's id.
                    reader.GetString(reader.GetOrdinal("LastName")),        // The Student's last name.
                    reader.GetString(reader.GetOrdinal("FirstMidName")),    // The Student's first and midle names.
                    reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")) // The Student's enrollment date.
                ),
                reader.GetInt32(reader.GetOrdinal("Grade"))                 // The Student's erollment grade.
            );
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override Enrollment BuildUknownModelObject()
        {
            return new Enrollment();
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override string BuildCompleteSelectAndFromClauses()
        {
            return "select     en.EnrollmentID " +
                   ",          en.Grade " +
                   ",          co.CourseID " +
                   ",          co.Title " +
                   ",          co.Credits " +
                   ",          de.DepartmentID " +
                   ",          de.Name " +
                   ",          de.Budget " +
                   ",          st.StudentID " +
                   ",          st.FirstMidName " +
                   ",          st.LastName " +
                   ",          st.EnrollmentDate " +
                   "from       Enrollments     en " +
                   "inner join Courses         co " +
                   "on         en.CourseID     = co.CourseID " +
                   "inner join Students        st " +
                   "on         en.StudentID    = st.StudentID " +
                   "inner join Departments     de " +
                   "on         co.DepartmentID = de.DepartmentID ";
        }
    }
}