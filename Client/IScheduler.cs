using System;

namespace Client
{
    /// <summary>
    /// Interface for scheduling actions
    /// </summary>
    public interface IScheduler
    {
        /// <summary>Schedule action to be executed on an interval</summary>
        /// <param name="interval">interval to execute the action</param>
        /// <param name="action">action to execute</param>
        void Every(TimeSpan interval, Action action);
    }
}
