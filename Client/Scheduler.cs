using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// <see cref="IScheduler"/> implementation using a <see cref="System.Threading.Tasks.Task"/>
    /// </summary>
    public class Scheduler : IScheduler
    {
        /// <summary>Schedule action to be executed on an interval</summary>
        /// <param name="interval">interval to execute the action</param>
        /// <param name="action">action to execute</param>
        public void Every(TimeSpan interval, Action action)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    action();
                    Thread.Sleep(interval);
                }
            }, TaskCreationOptions.AttachedToParent);
        }
    }
}
