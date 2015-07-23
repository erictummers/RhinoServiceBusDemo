using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Service.test
{
    [TestClass]
    public class ServiceBootstrapperTest : ServiceBootstrapper
    {
        public ServiceBootstrapperTest() : base() { CreateContainer(); }
        //public ServiceBootstrapperTest(IContainer container) : base(container) { }

        [TestMethod]
        public void ConfigureContainer_Finds_FileSystem()
        {
            var testObject = new ServiceBootstrapperTest();
            testObject.ConfigureContainer();

            var instance = GetInstance<IFileSystem>();

            Assert.IsNotNull(instance);
        }
    }
}
