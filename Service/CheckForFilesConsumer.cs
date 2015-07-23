using Common.Logging;
using RhinoServiceBusDemo.Messages;
using Rhino.ServiceBus;
using System.Linq;

namespace Service
{
    /// <summary>
    /// Consumer to handler the <see cref="CheckForFilesCommand"/> message
    /// </summary>
    public class CheckForFilesConsumer : ConsumerOf<CheckForFilesCommand>
    {
        private readonly IServiceBus _serviceBus;
        private readonly IFileSystem _fileSystem;
        private ILog _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckForFilesConsumer"/> class.
        /// </summary>
        /// <param name="serviceBus">The service bus.</param>
        /// <param name="fileSystem">The file system.</param>
        public CheckForFilesConsumer(IServiceBus serviceBus, IFileSystem fileSystem)
        {
            _serviceBus = serviceBus;
            _fileSystem = fileSystem;
        }

        /// <summary>Consumes the specified message.</summary>
        /// <param name="message">The message.</param>
        public void Consume(CheckForFilesCommand message)
        {
            _logger.Info("CheckForFilesCommand received");
            // create MoveFileCommand for every cs file in the rootDir
            var rootDir = @"D:\RhinoServiceBusDemo";
            var files = _fileSystem.GetFiles(rootDir, "*.cs");
            if (files != null && files.Any())
            {
                var messages = files.Select(x => new MoveFileCommand { PathToFile = x }).ToArray();
                // send the messages
                _logger.InfoFormat("Sending {0} MoveFileCommand(s)", messages.Length);
                _serviceBus.Send(messages);
            }
        }
    }
}
