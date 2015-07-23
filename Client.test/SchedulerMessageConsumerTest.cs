using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.ServiceBus;
using RhinoServiceBusDemo.Messages;
using System;

namespace Client.test
{
    [TestClass]
    public class SchedulerMessageConsumerTest
    {
        [TestMethod]
        public void Consume_Sends_CheckForFilesCommand()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var message = new SchedulerMessage();
            var testObject = new SchedulerMessageConsumer(fakeBus);

            testObject.Consume(message);

            fakeBus.AssertWasCalled(x => x.Send(Arg<CheckForFilesCommand>.Is.NotNull));
        }
    }
}
