using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleMessageBus.Utils
{
    public class ReflectionUtil : IReflectionUtil
    {
        public IEnumerable<Assembly> GetAssemblies()
        {
             var assemblies = System.Reflection.Assembly.GetEntryAssembly().GetReferencedAssemblies()
                    .Select(a => Assembly.Load(a))
                    .Append(Assembly.GetEntryAssembly());
                    
            return assemblies;
        }

        public IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes());
        }

        public IEnumerable<Type> InterfaceLookup(Type t)
        {
            return Assembly.GetEntryAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(t));
        }
    }
}