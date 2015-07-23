using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.ServiceBus;
using StructureMap;
using Rhino.Mocks;

namespace Client.test
{
    [TestClass]
    public class ClientBootstrapperTest : ClientBootstrapper
    {
        public ClientBootstrapperTest() : base() { CreateContainer(); }
        public ClientBootstrapperTest(IContainer container) : base(container) { }

        [TestMethod]
        public void ConfigureContainer_Finds_Scheduler()
        {
            var testObject = new ClientBootstrapperTest();
            testObject.ConfigureContainer();

            var instance = GetInstance<IScheduler>();

            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void OnEndStart_ScheduleEvery_Called()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var fakeScheduler = MockRepository.GenerateStub<IScheduler>();
            var fakeContainer = new Container();
            fakeContainer.Inject<IServiceBus>(fakeBus);
            fakeContainer.Inject<IScheduler>(fakeScheduler);
            var testObject = new ClientBootstrapperTest(fakeContainer);

            testObject.OnEndStart();

            fakeScheduler.AssertWasCalled(x => x.Every(
                Arg<TimeSpan>.Is.Anything, Arg<Action>.Is.NotNull));
        }

        [TestMethod]
        public void OnEndStart_SendSchedulerMessage_Scheduled()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var fakeScheduler = MockRepository.GenerateStub<IScheduler>();
            var fakeContainer = new Container();
            fakeContainer.Inject<IServiceBus>(fakeBus);
            fakeContainer.Inject<IScheduler>(fakeScheduler);
            var testObject = new ClientBootstrapperTest(fakeContainer);
            testObject.OnEndStart();

            // get the action from the arguments
            var args = fakeScheduler.GetArgumentsForCallsMadeOn(x => x.Every(
                Arg<TimeSpan>.Is.Anything, Arg<Action>.Is.NotNull));
            var action = args[0][1] as Action;
            // execute the action
            action();

            fakeBus.AssertWasCalled(x => x.Send(Arg<SchedulerMessage>.Is.NotNull));
        }
    }
}
