using Common.Logging;
using Rhino.ServiceBus;
using RhinoServiceBusDemo.Messages;

namespace Service
{
    /// <summary>
    /// Consumer to handler the <see cref="MoveFileCommand"/> message
    /// </summary>
    public class MoveFileCommandConsumer : ConsumerOf<MoveFileCommand>
    {
        private readonly IFileSystem _fileSystem;
        private ILog _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveFileCommandConsumer"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public MoveFileCommandConsumer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>Consumes the specified message.</summary>
        /// <param name="message">The message.</param>
        public void Consume(MoveFileCommand message)
        {
            var source = message.PathToFile;
            var destination = message.PathToFile.Replace("D:", "C:");
            _logger.InfoFormat("Moving {0} to {1}", source, destination);
            _fileSystem.MoveFile(source, destination);
        }
    }
}
