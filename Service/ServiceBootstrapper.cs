using Common.Logging;
using Rhino.ServiceBus.StructureMap;
using StructureMap;

namespace Service
{
    /// <summary>
    /// Used to Bootstrap the service service bus
    /// </summary>
    /// <remarks>
    /// Rhino.ServiceBus.Host.exe will load this as it is the only
    /// implementation of AbstractBootStrapper
    /// </remarks>
    public class ServiceBootstrapper : StructureMapBootStrapper
    {
        private ILog _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBootstrapper"/> class.
        /// </summary>
        public ServiceBootstrapper() : base() { }
        //public ServiceBootstrapper(IContainer container) : base(container) { }

        // StructureMap configuration
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            _logger.Debug("ConfigureContainer");
            // scan current assembly for consumers and interface/implementation pairs
            Container.Configure(x =>
            {
                x.Scan(s =>
                {
                    s.AssemblyContainingType<ServiceBootstrapper>();
                    s.WithDefaultConventions();
                });
            });
        }
    }
}
