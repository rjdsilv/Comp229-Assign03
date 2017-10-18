namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : GenericModel
    /// <b>Description</b>: Generic model class containing all the common attributes and methods for all the model classes.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    public abstract class GenericModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes the GenericModel with an empty id.
        /// </summary>
        protected GenericModel()
        {
            Id = 0;
        }
    }
}