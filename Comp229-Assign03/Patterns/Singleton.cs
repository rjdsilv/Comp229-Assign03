using System;
using System.Reflection;

namespace Comp229_Assign03.Patterns
{
    public abstract class Singleton<TSingleton>
    {
        protected static TSingleton INSTANCE = default(TSingleton);

        public static TSingleton GetInstance()
        {

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