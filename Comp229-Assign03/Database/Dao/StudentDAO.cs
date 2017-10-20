using System;
using System.Data.SqlClient;
using Comp229_Assign03.Database.Model;
using Comp229_Assign03.Patterns;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : StudentDAO
    /// <b>Description</b>: DAO class for dealing with database operations regarding the Students table.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public class StudentDAO : GenericDAO<Student, StudentDAO>
    {
        /// <summary>
        /// Creates a new instance of the StudentDAO class.
        /// </summary>
        protected StudentDAO()
        {
            modelName = "Student";
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildFindAllCommand(SqlConnection cnn)
        {
            string cmdText = "select StudentID " +
                             ",      LastName " +
                             ",      FirstMidName " +
                             ",      EnrollmentDate " +
                             "from   Students";
            SqlCommand cmd = new SqlCommand(cmdText, cnn);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildFindByIdCommand(SqlConnection cnn, int id)
        {
            string cmdText = "select StudentID " +
                             ",      LastName " +
                             ",      FirstMidName " +
                             ",      EnrollmentDate " +
                             "from   Students " +
                             "where  StudentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildDeleteCommand(SqlConnection cnn, Student modelObject)
        {
            string cmdText = "delete from Students where StudentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildInsertCommand(SqlConnection cnn, Student modelObject)
        {
            string cmdText = "insert into Students(LastName, FirstMidName, EnrollmentDate) values(@LastName, @FirstMidName, GetDate())";
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@LastName", modelObject.LastName);
            AddCommandParameter(cmd, "@FirstMidName", modelObject.FirstMidName);

            return cmd;
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override SqlCommand BuildUpdateCommand(SqlConnection cnn, Student modelObject)
        {
            string cmdText = "update Students set LastName = @LastName, FirstMidName = @FirstMidName, EnrollmentDate = @EnrollmentDate where StudentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, "@LastName", modelObject.LastName);
            AddCommandParameter(cmd, "@FirstMidName", modelObject.FirstMidName);
            AddCommandParameter(cmd, "@EnrollmentDate", modelObject.EnrollmentDateTime);
            AddCommandParameter(cmd, ID_PARAM, modelObject.Id);

            return cmd;
        }

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        protected override Student BuildObjectFromReader(SqlDataReader reader)
        {
            return new Student(
                reader.GetInt32(reader.GetOrdinal("StudentID")),              // The students's id
                reader.GetString(reader.GetOrdinal("LastName")),              // The students's last name
                reader.GetString(reader.GetOrdinal("FirstMidName")), // The students's first and middle names
                reader.GetDateTime(reader.GetOrdinal("EnrollmentDate"))       // The students's enrollment date
            );
        }

        ///
        /// <see cref="GenericDAO{TModel}" />
        ///
        protected override Student BuildUknownModelObject()
        {
            return new Student();
        }
    }
}