using System.Collections.Generic;
using Comp229_Assign03.Database.Model;
using System.Configuration;
using System.Data.SqlClient;
using System;
using Comp229_Assign03.Database.Exception;
using Comp229_Assign03.Patterns;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : CourseDAO
    /// <b>Description</b>: Abstract class containing all the commom properties the all DAO classes.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    /// <typeparam name="TModel">The model class to be used.</typeparam>
    /// <typeparam name="TSingleton">The singleton specification.</typeparam>
    public abstract class GenericDAO<TModel, TSingleton> : Singleton<TSingleton>, IGenericDAO<TModel> where TModel : GenericModel
    {
        // Constant declaration.
        protected const string ID_PARAM = "@Id";

        // Attributes declaration.
        protected string modelName = "";

        // The connection string defined in Web.config.
        protected string connectionString = ConfigurationManager.ConnectionStrings["Assignment03CnnStr"].ConnectionString;

        // Delegate declarations.
        protected delegate SqlCommand BuildNonQueryCommand(SqlConnection cnn, TModel modelObject);

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public List<TModel> FindAll()
        {
            List<TModel> allObjects = new List<TModel>();

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
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    allObjects.Add(BuildObjectFromReader(reader));
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(string.Format("An error has occurred when getting all the {0}s from the database! Please check if your database is online and set up correctly.", modelName), ex);
            }

            return allObjects;

        }

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public TModel FindById(int id)
        {
            TModel course = BuildUknownModelObject();

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
                            if (reader.HasRows && reader.Read())
                            {
                                course = BuildObjectFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(string.Format("An error has occurred when getting the {0} with Id = {1} from the database! Please check if your database is online and set up correctly.", modelName, id), ex);
            }

            return course;
        }

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public void Delete(TModel modelObject)
        {
            ExecuteNonQueryCommand(modelObject, string.Format("An error has occurred when deleting the {0} {1} from the database! Please check if your database is online and set up correctly.", modelName, modelObject), BuildDeleteCommand);
        }

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public void Insert(TModel modelObject)
        {
            ExecuteNonQueryCommand(modelObject, string.Format("An error has occurred when inserting the {0} {1} in the database! Please check if your database is online and set up correctly.", modelName, modelObject), BuildInsertCommand);
        }

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        public void Update(TModel modelObject)
        {
            ExecuteNonQueryCommand(modelObject, string.Format("An error has occurred when updating the {0} {1} in the database! Please check if your database is online and set up correctly.", modelName, modelObject), BuildUpdateCommand);
        }

        ///
        /// <see cref="IGenericDAO{TModel}" />
        ///
        protected abstract string BuildCompleteSelectAndFromClauses();

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
        /// Builds an model object from a given reader with recorsds returned from the database.
        /// </summary>
        /// <param name="reader">The reader to be used in order to create the model object</param>
        /// <returns>The object just created</returns>
        protected abstract TModel BuildObjectFromReader(SqlDataReader reader);

        /// <summary>
        /// Builds an unknown (empty) model object.
        /// </summary>
        /// <returns>The empty model object created.</returns>
        protected abstract TModel BuildUknownModelObject();

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
        /// <param name="BuildNonQueryCommandMethod">The method that will be invoked to build the non query command</param>
        private void ExecuteNonQueryCommand(TModel modelObject, string exceptionMessage, BuildNonQueryCommand BuildNonQueryCommandMethod)
        {
            try
            {
                // Disposes and closes automatically the connection when exiting the using statement.
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    // Disposes automatically the command when exiting the using statement.
                    using (SqlCommand cmd = BuildNonQueryCommandMethod(cnn, modelObject))
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