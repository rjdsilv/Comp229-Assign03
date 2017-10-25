using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Database.Exception;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : CourseDAO
    /// <b>Description</b>: DAO class for dealing with database operations regarding the Courses table.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class CourseDAO : GenericDAO<Course, CourseDAO>, ICourseDAO
    {
        /// <summary>
        /// Creates a new instance of the CourseDAO class.
        /// </summary>
        private CourseDAO()
        {
            ModelName = "Course";
        }

        ///
        /// <see cref="ICourseDAO" />
        ///
        public List<Course> FindAllCoursesNotEnrolledForStudent(Student student)
        {
            string whereClause = "where CourseID not in (" +
                                 "    select CourseID" +
                                 "    from   Enrollments " +
                                 "    where  StudentID = @StudentID " +
                                 ")";
            List<Course> allCourses = new List<Course>();

            try
            {
                // Disposes and closes automatically the connection when exiting the using statement.
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    // Disposes automatically the command when exiting the using statement.
                    using (SqlCommand cmd = new SqlCommand(BuildCompleteSelectAndFromClauses() + whereClause, cnn))
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
                                    allCourses.Add(BuildObjectFromReader(reader));
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(string.Format("An error has occurred when getting from the database all the {0}s not enrolled for the student {1}, {2}! Please check if your database is online and set up correctly.", ModelName, student.LastName, student.FirstMidName), ex);
            }

            return allCourses;
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
            string cmdText = BuildCompleteSelectAndFromClauses() + " where co.CourseID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildDeleteCommand(SqlConnection cnn, SqlTransaction tran, Course modelObject)
        {
            string cmdText = "delete from Courses where CourseID = " + ID_PARAM;
            SqlCommand cmd = null != tran ? new SqlCommand(cmdText, cnn, tran)  : new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildInsertCommand(SqlConnection cnn, SqlTransaction tran, Course modelObject)
        {
            string cmdText = "insert into Courses(Title, Credits, DepartmentID) values(@Title, @Credits, @DepartmentID)";
            SqlCommand cmd = null != tran ? new SqlCommand(cmdText, cnn, tran) : new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@Title", modelObject.Title);
            AddCommandParameter(cmd, "@Credits", modelObject.Credits);
            AddCommandParameter(cmd, "@DepartmentID", modelObject.Department.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildUpdateCommand(SqlConnection cnn, SqlTransaction tran, Course modelObject)
        {
            string cmdText = "update Courses set Title = @Title, Credits = @Credits, DepartmentID = @DepartmentID where CourseID = " + ID_PARAM;
            SqlCommand cmd = null != tran ? new SqlCommand(cmdText, cnn, tran) : new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@Title", modelObject.Title);
            AddCommandParameter(cmd, "@Credits", modelObject.Credits);
            AddCommandParameter(cmd, "@DepartmentID", modelObject.Department.Id);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override Course BuildObjectFromReader(SqlDataReader reader)
        {
            return new Course(
                reader.GetInt32(reader.GetOrdinal("CourseID")),         // The course's id
                reader.GetString(reader.GetOrdinal("Title")),           // The course's title
                reader.GetInt32(reader.GetOrdinal("Credits")),          // The course's credits
                new Department(
                    reader.GetInt32(reader.GetOrdinal("DepartmentID")), // The department's id.
                    reader.GetString(reader.GetOrdinal("Name")),        // The department's name.
                    reader.GetDecimal(reader.GetOrdinal("Budget"))      // The department's budget.
                )
            );
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override Course BuildUknownModelObject()
        {
            return new Course();
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override string BuildCompleteSelectAndFromClauses()
        {
            return "select     co.CourseID " +
                   ",          co.Title " +
                   ",          co.Credits " +
                   ",          de.DepartmentID " +
                   ",          de.Name " +
                   ",          de.Budget " +
                   "from       Courses     co " +
                   "inner join Departments de " +
                   "on         co.DepartmentID = de.DepartmentID ";
        }
    }
}