using Fivet.Dao;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{
    ///<sumary>
    /// The Implementation of the Contratos
    ///</sumary>
    public class ContratosImpl : ContratosDisp_
    {
        ///<sumary>
        /// Logger
        ///</sumary>
        private readonly ILogger<ContratosImpl> _logger;

        ///<sumary>
        /// The Provider of DbContext
        ///<sumary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        ///<sumary>
        /// The Constructor
        ///<sumary>
        ///<param name="logger"></param>
        ///<param name="serviceScopeFactory"></param>
        public ContratosImpl(ILogger<ContratosImpl> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _logger.LogDebug("Building th ContratosImpl ..");
            _serviceScopeFactory = serviceScopeFactory;

            //Create the database
            _logger.LogInformation("Creating the Database ..");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Database.EnsureCreated();
                fc.SaveChanges();
            }
            _logger.LogDebug("Done.");
        }

        /// <summary>
        /// Create the Persona
        /// </summary>
        /// <param name="persona">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
        public override Persona crearPersona(Persona persona, Current current = null)
        {
            //Using the local scope
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Personas.Add(persona);
                fc.SaveChanges();
                return persona;
            }
        }

        /// <summary>
        /// Create  the Control
        /// </summary>
        /// <param name="numeroFicha"></param>
        /// <param name="control"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Control crearControl(int numeroFicha, Control control, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Create the Ficha
        /// </summary>
        /// <param name="ficha"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Ficha crearFicha(Ficha ficha, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get a Ficha
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Ficha obtenerFicha(int numero, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get a Persona
        /// </summary>
        /// <param name="rut"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Persona obtenerPersona(string rut, Current current = null)
        {
            throw new System.NotImplementedException();
        }
    }
}