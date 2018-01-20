using System;
using FluentAssertions;
using Grumpy.Common.Interfaces;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class ProcessInformationTests
    {
        [Fact]
        public void CanGetProcessInformation()
        {
            var cut = CreateProcessInformation();

            cut.MachineName.Should().NotBeNull();
            cut.UserName.Should().NotBeNull();
            cut.ApplicationName.Should().NotBeNull();
            cut.DomainName.Should().NotBeNull();
            cut.Id.Should().BeGreaterThan(0);
            cut.InitialProgram.Should().NotBeNull();
            cut.ProcessName.Should().NotBeNull();
            cut.WorkingDirectory.Should().NotBeNull();
        }

        private static IProcessInformation CreateProcessInformation()
        {
            return new ProcessInformation();
        }
    }
}