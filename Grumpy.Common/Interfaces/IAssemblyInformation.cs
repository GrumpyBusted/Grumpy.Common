namespace Grumpy.Common.Interfaces
{
    /// <summary>
    /// Assembly Information
    /// </summary>
    public interface IAssemblyInformation
    {
        /// <summary>
        /// Assembly Description
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// Assembly Title
        /// </summary>
        string Title { get; }
       
        /// <summary>
        /// Assembly Version
        /// </summary>
        string Version { get; }
    }
}