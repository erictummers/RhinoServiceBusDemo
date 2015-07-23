using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Client.test
{
    [TestClass]
    public class SchedulerTest
    {
        [TestMethod]
        public void Scheduler_Every_ExecutesAction()
        {
            var delayForTaskToStart = TimeSpan.FromMilliseconds(100);
            var timeSpanShorterThan100Miliseconds = TimeSpan.FromMilliseconds(1);
            var testObject = new Scheduler();

            var called = false;
            testObject.Every(timeSpanShorterThan100Miliseconds, () => called = true);
            System.Threading.Thread.Sleep(delayForTaskToStart);

            Assert.IsTrue(called);
        }
    }
}
