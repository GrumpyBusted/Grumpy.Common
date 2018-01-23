using System;
using System.Reflection;
using Grumpy.Common.Interfaces;

namespace Grumpy.Common
{
    /// <inheritdoc />
    public class AssemblyInformation : IAssemblyInformation
    {
        /// <inheritdoc />
        public string Description { get; private set; }

        /// <inheritdoc />
        public string Title { get; private set; }

        /// <inheritdoc />
        public string Version { get; private set; }

        /// <inheritdoc />
        public AssemblyInformation()
        {
            Set(Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly());
        }

        /// <inheritdoc />
        public AssemblyInformation(Assembly assembly)
        {
            Set(assembly);
        }

        private void Set(Assembly assembly)
        {
            Description = GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly)?.Description ?? "Description not defined";
            Title = GetAssemblyAttribute<AssemblyTitleAttribute>(assembly)?.Title ?? $"TitleMissing.{UniqueKeyUtility.Generate()}";
            Version = GetAssemblyAttribute<AssemblyVersionAttribute>(assembly)?.Version ?? GetAssemblyAttribute<AssemblyFileVersionAttribute>(assembly)?.Version ?? "0.0";
        }

        private static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
        {
            return Attribute.IsDefined(assembly, typeof(T)) ? (T) Attribute.GetCustomAttribute(assembly, typeof(T)) : default(T);
        }
    }
}