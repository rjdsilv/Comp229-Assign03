using System.Collections.Generic;
using Comp229_Assign03.Database.Model;
using System.Configuration;
using System.Data.SqlClient;
using System;
using Comp229_Assign03.Database.Exception;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : CourseDAO
    /// <b>Description</b>: Abstract class containing all the commom properties the all DAO classes.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    /// <typeparam name="TModel">The model class to be used.</typeparam>
    public abstract class GenericDAO<TModel> : IGenericDAO<TModel> where TModel : GenericModel
    {
        // Constant declaration.
        protected const string ID_PARAM = "@Id";

        // The connection string defined in Web.config.
        protected string connectionString = ConfigurationManager.ConnectionStrings["Assignment03CnnStr"].ConnectionString;

        // Delegate declarations.
        protected delegate SqlCommand BuildNonQueryCommand(SqlConnection cnn, TModel modelObject);

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public abstract List<TModel> FindAll();

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public abstract TModel FindById(int id);

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public abstract void Delete(TModel modelObject);

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public abstract void Insert(TModel modelObject);

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public abstract void Update(TModel modelObject);

        /// <summary>
        /// Builds a specific delete command for the model object passed as parameter.
        /// </summary>
        /// <param name="cnn">The database connection object</param>
        /// <param name="modelObject">The object to be deleted from the database</param>
        /// <returns>The delete command built.</returns>
        /// <exception cref="Database.Exception.DatabaseException">If some error occurs during the delete operation.</exception>
        protected abstract SqlCommand BuildDeleteCommand(SqlConnection cnn, TModel modelObject);

        /// <summary>
        /// Builds a specific command for finding all the objects from the model object passed as parameter.
        /// </summary>
        /// <param name="cnn">The database connection object</param>
        /// <returns>The list of objects found or an empty list if no object is found on the database.</returns>
        /// <exception cref="Database.Exception.DatabaseException">If some error occurs when selecting the database.</exception>
        protected abstract SqlCommand BuildFindAllCommand(SqlConnection cnn);

        /// <summary>
        /// Builds a specific command for finding an object by its id from the model object passed as parameter.
        /// </summary>
        /// <param name="cnn">The database connection object.</param>
        /// <param name="id">The id of the object to be found.</param>
        /// <returns>The course found or the unknown course if none is found.</returns>
        /// <exception cref="Database.Exception.DatabaseException">If some error occurs when selecting the database.</exception>
        protected abstract SqlCommand BuildFindByIdCommand(SqlConnection cnn, int id);

        /// <summary>
        /// Builds a specific insert command for the model object passed as parameter.
        /// </summary>
        /// <param name="cnn">The database connection object</param>
        /// <param name="modelObject">The object to be inserted in the database</param>
        /// <returns>The insert command built.</returns>
        /// <exception cref="Database.Exception.DatabaseException">If some error occurs during the insert operation.</exception>
        protected abstract SqlCommand BuildInsertCommand(SqlConnection cnn, TModel modelObject);

        /// <summary>
        /// Builds a specific update command for the model object passed as parameter.
        /// </summary>
        /// <param name="cnn">The database connection object</param>
        /// <param name="modelObject">The object to be updated in the database</param>
        /// <returns>The update command built.</returns>
        /// <exception cref="Database.Exception.DatabaseException">If some error occurs during the update operation.</exception>
        protected abstract SqlCommand BuildUpdateCommand(SqlConnection cnn, TModel modelObject);

        /// <summary>
        /// Add a parameter to the SqlCommand given. This method will treat null values.
        /// </summary>
        /// <param name="cmd">The command to have the parameter inserted</param>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        protected void AddCommandParameter(SqlCommand cmd, string name, object value)
        {
            if (null != value)
            {
                cmd.Parameters.AddWithValue(name, value);
            }
            else
            {
                cmd.Parameters.AddWithValue(name, DBNull.Value);
            }
        }

        /// <summary>
        /// Executes a non-query Sql Command in order to insert, update or delete a record on the database.
        /// </summary>
        /// <param name="modelObject">The object to be inserted, deleted or updated.</param>
        /// <param name="exceptionMessage">The message to be thrown by the exception.</param>
        /// <param name="BuildNonQueryMethod">The method that will be invoked to build the non query command</param>
        protected void ExecuteNonQueryCommand(TModel modelObject, string exceptionMessage, BuildNonQueryCommand BuildNonQueryMethod)
        {
            try
            {
                // Disposes and closes automatically the connection when exiting the using statement.
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    // Disposes automatically the command when exiting the using statement.
                    using (SqlCommand cmd = BuildNonQueryMethod(cnn, modelObject))
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(exceptionMessage, ex);
            }
        }
    }
}