using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleMessageBus.Utils
{
    public interface IReflectionUtil
    {
        IEnumerable<Assembly> GetAssemblies();
        IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies);
        IEnumerable<Type> InterfaceLookup(Type t);

    }
}