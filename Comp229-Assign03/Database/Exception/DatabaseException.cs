using System;
using System.Runtime.Serialization;

namespace Comp229_Assign03.Database.Exception
{
    /// <summary>
    /// <b>Class</b>      : DatabaseException
    /// <b>Description</b>: Class that will encapsulate any application's database thrown exception.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    [Serializable()]
    public class DatabaseException : System.Exception
    {
        /// <summary>
        /// Creates a new instance of DatabaseException
        /// </summary>
        public DatabaseException() : base() { }

        /// <summary>
        /// Creates a new instance of DatabaseException using an user defined message.
        /// </summary>
        /// <param name="message">The message to be shown by the exception.</param>
        public DatabaseException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of DatabaseException using an user defined message and the exception that caused it.
        /// </summary>
        /// <param name="message">The message to be shown by the exception.</param>
        /// <param name="cause">The exception's cause.</param>
        public DatabaseException(string message, System.Exception cause) : base(message, cause) { }


        /// <summary>
        /// Creates a new instance of DatabaseException making it possible to serialize it.
        /// </summary>
        /// <param name="info">The serialization's information.</param>
        /// <param name="context">The serialization's streaming context.</param>
        protected DatabaseException(SerializationInfo info, StreamingContext context) { }
    }
}