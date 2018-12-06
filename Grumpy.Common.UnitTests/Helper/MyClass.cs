using System.Runtime.InteropServices.ComTypes;

namespace Grumpy.Common.UnitTests.Helper
{
    public class MyClass
    {
        public string Data { get; private set; }

        public string PublicMethod(string data)
        {
            Data = data;

            return Data;
        }

        protected string ProtectedMethod(string data)
        {
            Data = data;

            return Data;
        }

        internal string InternalMethod(string data)
        {
            Data = data;

            return Data;
        }

        private string PrivateMethod(string data)
        {
            Data = data;

            return Data;
        }
    }
}
