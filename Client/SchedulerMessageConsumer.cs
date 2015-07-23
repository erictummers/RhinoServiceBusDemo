using Common.Logging;
using Rhino.ServiceBus;
using RhinoServiceBusDemo.Messages;

namespace Client
{
    /// <summary>
    /// Consumer to handler the messages from the <see cref="Scheduler"/>
    /// </summary>
    public class SchedulerMessageConsumer : ConsumerOf<SchedulerMessage>
    {
        private readonly IServiceBus _serviceBus;
        private ILog _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerMessageConsumer"/> class.
        /// </summary>
        /// <param name="serviceBus">The service bus.</param>
        public SchedulerMessageConsumer(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        /// <summary>Consumes the specified message.</summary>
        /// <param name="message">The message from the scheduler.</param>
        public void Consume(SchedulerMessage message)
        {
            _logger.Info("Sending CheckForFilesCommand");
            _serviceBus.Send(new CheckForFilesCommand());
        }
    }
}
