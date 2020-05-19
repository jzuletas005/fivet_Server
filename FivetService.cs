using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace fivetServer
{
    internal class FivetService : IHostedService
    {
        private readonly ILogger<FivetService> _logger;

        private readonly int _port = 8080;

        private readonly Communicator _communicator;

        public FivetService(ILogger<FivetService> logger)
        {
            _logger = logger;
            _communicator = buildComunicator();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService ..");

            var adapter = _communicator.createObjectAdapterWithEndpoints("TheSystem", "tcp -z -t 15000 -p" + _port);

            TheSystem theSystem = new TheSystemImpl();

            adapter.add(theSystem, Util.stringToIdentity("TheSystem"));

            adapter.activate();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stoping the FivetService ..");

            _communicator.shutdown();

            _logger.LogDebug("Communication Stoped");

            return Task.CompletedTask;
        }

        private Communicator buildComunicator()
        {
            Properties prop = Util.createProperties();

            InitializationData initializationData = new InitializationData();
            initializationData.properties = prop;

            return Ice.Util.initialize(initializationData);
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