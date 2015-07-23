using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhinoServiceBusDemo.Messages;
using Rhino.ServiceBus;
using Rhino.Mocks;


namespace Service.test
{
    [TestClass]
    public class MoveFileCommandConsumerTest
    {
        private MoveFileCommandConsumer CreateTestObject(IFileSystem fileSystem)
        {
            var testObject = new MoveFileCommandConsumer(fileSystem);
            return testObject;
        }

        [TestMethod]
        public void Consume_MoveFileCommand_Executed()
        {
            var fakeFileSystem = MockRepository.GenerateStub<IFileSystem>();
            var testObject = CreateTestObject(fakeFileSystem);
            var file = @"D:\test\temp.txt";
            var fileOnDiskLetterC = @"C:\test\temp.txt";
            var message = new MoveFileCommand { PathToFile = file };

            testObject.Consume(message);

            fakeFileSystem.AssertWasCalled(x => 
                x.MoveFile(Arg<string>.Is.Equal(file), Arg<string>.Is.Equal(fileOnDiskLetterC)));
        }
    }
}
