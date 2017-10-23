using System;

namespace Comp229_Assign03.Database.Model
{
    /// <summary>
    /// <b>Class</b>      : GenericModel
    /// <b>Description</b>: Generic model class containing all the common attributes and methods for all the model classes.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Identifies if the current Model is the unknown or not.
        /// </summary>
        /// <returns>><b>True</b> if the model is the Unknown one.<b>False</b> otherwise.</returns>
        public bool IsUnknown()
        {
            return 0 == Id;
        }
    }
}