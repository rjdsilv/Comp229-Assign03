using Comp229_Assign03.Database.Exception;
using Comp229_Assign03.Database.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : CourseDAO
    /// <b>Description</b>: DAO class for dealing with database operations regarding the Courses table.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class CourseDAO : GenericDAO<Course>
    {
        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        public override List<Course> FindAll()
        {
            List<Course> allCourses = new List<Course>();

            try
            {
                // Disposes and closes automatically the connection when exiting the using statement.
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    // Disposes automatically the command when exiting the using statement.
                    using (SqlCommand cmd = BuildFindAllCommand(cnn))
                    {
                        cnn.Open();

                        // Disposes and closes automatically the data reader when exiting the using statement.
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.NextResult())
                            {
                                allCourses.Add(BuildCourseFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException("An error has occurred when getting all the Courses from the database!", ex);
            }

            return allCourses;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        public override Course FindById(int id)
        {
            Course course = new Course();

            try
            {
                // Disposes and closes automatically the connection when exiting the using statement.
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    // Disposes automatically the command when exiting the using statement.
                    using (SqlCommand cmd = BuildFindByIdCommand(cnn, id))
                    {
                        cnn.Open();

                        // Disposes and closes automatically the data reader when exiting the using statement.
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.NextResult())
                            {
                                course = BuildCourseFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException("An error has occurred when getting the Course with Id = " + id + " from the database!", ex);
            }

            return course;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        public override void Delete(Course modelObject)
        {
            ExecuteNonQueryCommand(modelObject, "An error has occurred when deleting the Course " + modelObject + " from the database!", BuildDeleteCommand);
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        public override void Insert(Course modelObject)
        {
            ExecuteNonQueryCommand(modelObject, "An error has occurred when inserting the Course " + modelObject + " in the database!", BuildInsertCommand);
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        public override void Update(Course modelObject)
        {
            ExecuteNonQueryCommand(modelObject, "An error has occurred when updating the Course " + modelObject + " in the database!", BuildUpdateCommand);
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildFindAllCommand(SqlConnection cnn)
        {
            string cmdText = "select     co.CourseID " +
                             ",          co.Title " +
                             ",          co.Credits " +
                             ",          de.DepartmentID " +
                             ",          de.Name " +
                             ",          de.Budget " +
                             "from       Courses     co " +
                             "inner join Departments de " +
                             "on         co.DepartmentID = de.DepartmentID";
            SqlCommand cmd = new SqlCommand(cmdText, cnn);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildFindByIdCommand(SqlConnection cnn, int id)
        {
            string cmdText = "select     co.CourseID " +
                             ",          co.Title " +
                             ",          co.Credits " +
                             ",          de.DepartmentID " +
                             ",          de.Name " +
                             ",          de.Budget " +
                             "from       Courses     co " +
                             "inner join Departments de " +
                             "on         co.DepartmentID = de.DepartmentID " +
                             "where      co.CourseID     = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildDeleteCommand(SqlConnection cnn, Course modelObject)
        {
            string cmdText = "delete from Courses where CourseID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildInsertCommand(SqlConnection cnn, Course modelObject)
        {
            string cmdText = "insert into Courses(CourseID, Title, Credits, DepartmentID) values(" + ID_PARAM + "@Title, @Credits, @DepartmentID)";
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);
            AddCommandParameter(cmd, "@Title", modelObject.Title);
            AddCommandParameter(cmd, "@Credits", modelObject.Credits);
            AddCommandParameter(cmd, "@DepartmentID", modelObject.Department.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildUpdateCommand(SqlConnection cnn, Course modelObject)
        {
            string cmdText = "update Courses set Title = @Title, Credits = @Credits, DepartmentID = @DepartmentID where CourseID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@Title", modelObject.Title);
            AddCommandParameter(cmd, "@Credits", modelObject.Credits);
            AddCommandParameter(cmd, "@DepartmentID", modelObject.Department.Id);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        /// <summary>
        /// Builds a course from a given reader with recorsds returned from the database.
        /// </summary>
        /// <param name="reader">The reader to be used in order to create the course</param>
        /// <returns>The course just created</returns>
        private Course BuildCourseFromReader(SqlDataReader reader)
        {
            return new Course(
                reader.GetInt32(reader.GetOrdinal("CourseID")),         // The course's id
                reader.GetString(reader.GetOrdinal("Title")),           // The course's title
                reader.GetInt32(reader.GetOrdinal("Credits")),          // The course's credits
                new Department(
                    reader.GetInt32(reader.GetOrdinal("DepartmentID")), // The department's id.
                    reader.GetString(reader.GetOrdinal("Name")),        // The department's name.
                    reader.GetFloat(reader.GetOrdinal("Budget"))        // The department's budget.
                )
            );
        }
    }
}