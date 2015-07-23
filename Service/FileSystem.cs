using System.IO;

namespace Service
{
    /// <summary>
    /// <see cref="IFileSystem"/> implementation using <see cref="System.IO.Directory"/> 
    /// and <see cref="System.IO.File"/>
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <summary>Gets the files.</summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>Files in the path matching the searchPattern.</returns>
        public string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        /// <summary>Moves the file.</summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public void MoveFile(string source, string destination)
        {
            File.Move(source, destination);
        }
    }
}
