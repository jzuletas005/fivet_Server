using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace fivetServer
{   
    /// <summary>
    /// The Fivet Service
    /// </summary>
    internal class FivetService : IHostedService, IDisposable
    {   
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<FivetService> _logger;

        /// <summary>
        /// The Port
        /// </summary>
        private readonly int _port = 8080;

        /// <summary>
        /// The Communicator
        /// </summary>
        private readonly Communicator _communicator;

        /// <summary>
        /// The implementation of the System
        /// </summary>
        private readonly TheSystemDisp_ _theSystem;

        /// <summary>
        /// The implementation of the Contratos
        /// </summary>
        private readonly ContratosDisp_ _contratos;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="theSystem"></param>
        /// <param name="contratos"></param>
        public FivetService(ILogger<FivetService> logger, TheSystemDisp_ theSystem, ContratosDisp_ contratos)
        {
            _logger = logger;
            _logger.LogDebug("Building FivetService ..");
            _theSystem = theSystem;
            _contratos = contratos;
            _communicator = buildComunicator();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService ..");

            // The adapter
            var adapter = _communicator.createObjectAdapterWithEndpoints("TheSystem", "tcp -z -t 15000 -p" + _port);

            TheSystem theSystem = new TheSystemImpl();

            // Register in the communicator
            adapter.add(theSystem, Util.stringToIdentity("TheSystem"));

            //Activation
            adapter.activate();

            _theSystem.getDelay(0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stoping the FivetService ..");

            _communicator.shutdown();

            _logger.LogDebug("Communication Stoped");

            return Task.CompletedTask;
        }

        private Communicator buildComunicator()
        {
            _logger.LogDebug("Initializing Communicator v{0} ({1}) ..", Ice.Util.stringVersion(), Ice.Util.intVersion());

            Properties properties = Util.createProperties();

            InitializationData initializationData = new InitializationData();
            initializationData.properties = properties;

            return Ice.Util.initialize(initializationData);
        }

        /// <summary>
        /// Clear the memory
        /// </summary>
        public void Dispose()
        {
            _communicator.destroy();
        }
    }

    public class TheSystemImpl : TheSystemDisp_
    {
        public override long getDelay(long clienTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clienTime;
        }
    }

}