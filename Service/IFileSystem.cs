
namespace Service
{
    /// <summary>
    /// Interface for FileSystem actions
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>Gets the files.</summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>Files in the path matching the searchPattern.</returns>
        string[] GetFiles(string path, string searchPattern);
        /// <summary>Moves the file.</summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        void MoveFile(string source, string destination);
    }
}
