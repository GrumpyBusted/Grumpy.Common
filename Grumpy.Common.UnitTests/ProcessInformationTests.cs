using System;
using FluentAssertions;
using Grumpy.Common.Extensions;
using Grumpy.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class ProcessInformationTests
    {
        [TestMethod]
        public void CanGetProcessInformation()
        {
            var cut = CreateProcessInformation();

            cut.MachineName.Should().Be(Environment.MachineName);
            cut.UserName.Should().Be(Environment.UserName);
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