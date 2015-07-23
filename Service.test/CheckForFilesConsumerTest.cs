using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhinoServiceBusDemo.Messages;
using Rhino.ServiceBus;
using Rhino.Mocks;

namespace Service.test
{
    [TestClass]
    public class CheckForFilesConsumerTest
    {
        private CheckForFilesConsumer CreateTestObject(IServiceBus bus, string[] files = null)
        {
            var fileSystem = MockRepository.GenerateStub<IFileSystem>();
            fileSystem.Stub(x => x.GetFiles(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(files);
            var testObject = new CheckForFilesConsumer(bus, fileSystem);
            return testObject;
        }

        [TestMethod]
        public void Consume_NoFiles_DoeNotSendMessages()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var testObject = CreateTestObject(fakeBus);
            var message = new CheckForFilesCommand();
            
            testObject.Consume(message);

            fakeBus.AssertWasNotCalled(x => x.Send(Arg<MoveFileCommand>.Is.Anything));
        }

        [TestMethod]
        public void Consume_File_SendMoveFileCommand()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var file = new string[] { @"d:\temp\file.txt" };
            var testObject = CreateTestObject(fakeBus, file);
            var message = new CheckForFilesCommand();

            testObject.Consume(message);

            fakeBus.AssertWasCalled(x => x.Send(Arg<MoveFileCommand[]>.Is.NotNull));
        }

        [TestMethod]
        public void Consume_TwoFiles_SendTwoMessages()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var files = new string[] { @"d:\temp\file.txt", @"d:\temp\file2.txt" };
            var testObject = CreateTestObject(fakeBus, files);
            var message = new CheckForFilesCommand();

            testObject.Consume(message);

            fakeBus.AssertWasCalled(x =>
                x.Send(Arg<MoveFileCommand[]>.List.Count(Rhino.Mocks.Constraints.Is.Equal(2))));
        }

        [TestMethod]
        public void Consume_MoveFileCommand_PathToFileSet()
        {
            var fakeBus = MockRepository.GenerateStub<IServiceBus>();
            var fileName = @"d:\temp\file.txt";
            var file = new string[] { fileName };
            var testObject = CreateTestObject(fakeBus, file);
            var message = new CheckForFilesCommand();

            testObject.Consume(message);

            fakeBus.AssertWasCalled(x => 
                x.Send(Arg<MoveFileCommand[]>.Matches(msgs =>
                    msgs.Any(msg => msg.PathToFile.Equals(fileName))
                )));
        }
    }
}
