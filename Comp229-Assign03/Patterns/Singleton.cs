using System;
using System.Reflection;

namespace Comp229_Assign03.Patterns
{
    /// <summary>
    /// <b>Class</b>      : Singleton
    /// <b>Description</b>: Abstract class implementing the Singleton design pattern.
    /// <b>Author</b>     : Rodrigo Januario da Silva
    /// <b>Version</b>    : 1.0.0
    /// </summary>
    /// <typeparam name="TSingleton">The class type that should be instantiated</typeparam>
    public abstract class Singleton<TSingleton>
    {
        protected static TSingleton INSTANCE = default(TSingleton);

        /// <summary>
        /// Gets the singleton instance. If not yet created, create the instance dynamically and return it. Otherwise just return the existing instance.
        /// </summary>
        /// <returns>The singleton instance.</returns>
        public static TSingleton GetInstance()
        {
            // Using reflection to instantiate the child singleton class.
            if (null == INSTANCE)
            {
                Type type = typeof(TSingleton);
                ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                INSTANCE = (TSingleton) constructors[0].Invoke(new object[] { });
            }

            return INSTANCE;
        }
    }
}