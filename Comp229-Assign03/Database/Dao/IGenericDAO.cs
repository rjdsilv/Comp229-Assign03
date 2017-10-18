using Comp229_Assign03.Database.Model;
using System.Collections.Generic;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// <b>Class</b>      : CourseDAO
    /// <b>Description</b>: Interface containing the all the necessary methods to be implemented by a DAO class.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    /// <typeparam name="TModel">The model class to be used.</typeparam>
    interface IGenericDAO<TModel> where TModel : GenericModel
    {
        /// <summary>
        /// Finds a database object represented by TModel by its id.
        /// </summary>
        /// <param name="id">The identificator do be found.</param>
        /// <returns>The object found. UNKNOWN otherwise.</returns>
        /// <exception cref="Exception.DatabaseException">If any database error occurs.</exception>
        TModel FindById(int id);

        /// <summary>
        /// Finds all the existing objects in the database for the specified TModel.
        /// </summary>
        /// <returns>The list of objects found. An empty list.</returns>
        /// <exception cref="Exception.DatabaseException">If any database error occurs.</exception>
        List<TModel> FindAll();

        /// <summary>
        /// Inserts in the database the given object.
        /// </summary>
        /// <param name="modelObject">The object to be inserted on the database.</param>
        /// <exception cref="Exception.DatabaseException">If any database error occurs.</exception>
        void Insert(TModel modelObject);

        /// <summary>
        /// Updates the given object in the database.
        /// </summary>
        /// <param name="modelObject">The object to be updated on the database.</param>
        /// <exception cref="Exception.DatabaseException">If any database error occurs.</exception>
        void Update(TModel modelObject);

        /// <summary>
        /// Deletes the given object from the database.
        /// </summary>
        /// <param name="modelObject">The object to be deleted from the database.</param>
        /// <exception cref="Exception.DatabaseException">If any database error occurs.</exception>
        void Delete(TModel modelObject);
    }
}
