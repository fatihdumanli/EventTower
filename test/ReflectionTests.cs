using NUnit.Framework;
using SimpleMessageBus.Utils;
using System.Linq;
using System.Reflection;

namespace MessageBus.UnitTests
{
    public class ReflectionTests
    {

        private readonly ReflectionUtil reflectionUtil = new ReflectionUtil();

        [Test]
        public void Should_Return_All_Referenced_Assemblies()
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            assemblies.Append(Assembly.GetEntryAssembly().GetName());
            Assert.AreEqual(assemblies.Length + 1, reflectionUtil.GetAssemblies().ToList().Count);
        }

        [Test]
        public void Should_Return_All_Types()
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(w => Assembly.Load(w));
            var types = assemblies.SelectMany(w => w.GetTypes());
            Assert.AreEqual(types.Count(), reflectionUtil.GetTypes(assemblies).Count());
        }
    }
}
