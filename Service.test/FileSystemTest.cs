using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;

namespace Service.test
{
    [TestClass]
    public class FileSystemTest
    {
        [TestMethod]
        public void GetFiles_DirectoryGetFiles_called()
        {
            var testObject = new FileSystem();
            var someRandomPath = @"d:\temp\test";
            var someRandomPattern = "*.txt";

            var isMethodCalled = false;
            using(ShimsContext.Create())
            {
                System.IO.Fakes.ShimDirectory.GetFilesStringString =
                    (path, pattern) => { isMethodCalled = true; return null; };
                testObject.GetFiles(someRandomPath, someRandomPattern);
            }

            Assert.IsTrue(isMethodCalled);
        }

        [TestMethod]
        public void MoveFile_called()
        {
            var testObject = new FileSystem();
            var someRandomPath = @"d:\temp\test.txt";

            var isMethodCalled = false;
            using (ShimsContext.Create())
            {
                System.IO.Fakes.ShimFile.MoveStringString =
                    (source, destination) => isMethodCalled = true;
                testObject.MoveFile(someRandomPath, someRandomPath);
            }

            Assert.IsTrue(isMethodCalled);
        }
    }
}
