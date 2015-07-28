using System;

namespace RhinoServiceBusDemo.Messages
{
    /// <summary>
    /// Move file command
    /// </summary>
    public class MoveFileCommand
    {
        /// <summary>Gets or sets the path to file to move.</summary>
        public string PathToFile { get; set; }
    }
}
