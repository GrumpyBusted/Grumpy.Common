using System.Reflection;
using FluentAssertions;
using Grumpy.Common.Interfaces;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class AssemblyInformationTests
    {
        [Fact]
        public void CanGetAssemblyInformation()
        {
            var cut = CreateAssemblyInformation();

            cut.Description.Should().NotBeNull();
            cut.Version.Should().NotBeNull();
            cut.Title.Should().NotBeNull();
        }

        [Fact]
        public void CanGetAssemblyInformationForSpecifiedAssembly()
        {
            var cut = CreateAssemblyInformation(Assembly.GetCallingAssembly());

            cut.Description.Should().NotBeNull();
            cut.Version.Should().NotBeNull();
            cut.Title.Should().NotBeNull();
        }

        private static IAssemblyInformation CreateAssemblyInformation(Assembly assembly = null)
        {
            return assembly == null ? new AssemblyInformation() : new AssemblyInformation(assembly);
        }
    }
}