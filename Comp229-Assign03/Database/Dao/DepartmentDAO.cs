using System;
using System.Data.SqlClient;
using Comp229_Assign03.Database.Model;

namespace Comp229_Assign03.Database.Dao
{
    public class DepartmentDAO : GenericDAO<Department, DepartmentDAO>, IDepartmentDAO
    {
        /// <summary>
        /// Creates a new instance of the DepartmentDAO class.
        /// </summary>
        private DepartmentDAO()
        {
            ModelName = "Department";
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
            string cmdText = BuildCompleteSelectAndFromClauses() + " where DepartmentID = " + ID_PARAM;
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            AddCommandParameter(cmd, ID_PARAM, id);

            return cmd;
        }

        protected override SqlCommand BuildDeleteCommand(SqlConnection cnn, SqlTransaction tran, Department modelObject)
        {
            throw new NotImplementedException();
        }

        protected override SqlCommand BuildInsertCommand(SqlConnection cnn, SqlTransaction tran, Department modelObject)
        {
            throw new NotImplementedException();
        }

        protected override Department BuildUknownModelObject()
        {
            throw new NotImplementedException();
        }

        protected override SqlCommand BuildUpdateCommand(SqlConnection cnn, SqlTransaction tran, Department modelObject)
        {
            throw new NotImplementedException();
        }

        protected override Department BuildObjectFromReader(SqlDataReader reader)
        {
            return new Department(
                reader.GetInt32(reader.GetOrdinal("DepartmentID")), // The departments's id
                reader.GetString(reader.GetOrdinal("Name")),        // The departments's name
                reader.GetDecimal(reader.GetOrdinal("Budget"))      // The departments's budget
            );
        }

        protected override string BuildCompleteSelectAndFromClauses()
        {
            return "select     DepartmentID " +
                   ",          Name " +
                   ",          Budget " +
                   "from       Departments ";
        }
    }
}