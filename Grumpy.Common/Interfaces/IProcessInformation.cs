namespace Grumpy.Common.Interfaces
{
    /// <summary>
    /// Get information about the current process and session
    /// </summary>
    public interface IProcessInformation
    {
        /// <summary>
        /// Application Name
        /// </summary>
        string ApplicationName { get; }
        
        /// <summary>
        /// Logon Domain Name
        /// </summary>
        string DomainName { get; }
        
        /// <summary>
        /// Process Id
        /// </summary>
        int Id { get; }
        
        /// <summary>
        /// Initial Program
        /// </summary>
        string InitialProgram { get; }
        
        /// <summary>
        /// Machine Name
        /// </summary>
        string MachineName { get; }
        
        /// <summary>
        /// User Name
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Working Directory
        /// </summary>
        string WorkingDirectory { get; }

        /// <summary>
        /// Process Name
        /// </summary>
        string ProcessName { get; }
    }
}