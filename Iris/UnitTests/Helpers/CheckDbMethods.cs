using System;
using System.Reflection;
using Iris.Attributes;

namespace UnitTests.Helpers
{
    public static class CheckDbMethods
    {
        public static bool HasDbGetterDataAttribute(Type classType, string methodName)
        {
            var method = classType.GetMethod(methodName);

            return method != null && method.GetCustomAttribute(typeof(DbGetterDataAttribute)) != null;
        }
    }
}
