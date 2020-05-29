using System.Reflection;
using Fivet.ZeroIce.model;
using Microsoft.EntityFrameworkCore;

namespace Fivet.Dao
{
    ///<sumary>
    /// The Connection to FivetDataBase
    ///<sumary>
    public class FivetContext : DbContext
    {
        ///<sumary>
        /// The Connection to the database
        ///<sumary>
        ///<value></value>
        public DbSet<Persona> Personas {get; set; }

        ///<sumary>
        /// Configuration
        ///<param name="optionBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite
            optionsBuilder.UseSqlite("Data Source=fivet.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        ///<sumary>
        /// Create the ER from Entity
        ///<sumary>
        ///<param name="modelBuilder">to use</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Update the model
            modelBuilder.Entity<Persona>(p =>
            {
                //Primary Key
                p.HasKey(p => p.uid);
                // Index in Email
                p.Property(p => p.email).IsRequired();
                p.HasIndex(p => p.email).IsUnique();
            });

            // Insert the data
            modelBuilder.Entity<Persona>().HasData(
                new Persona()
                {
                    uid = 1,
                    nombre = "Javier",
                    apellido = "Zuleta",
                    direccion = "Nicolas Tirado 20",
                    email = "jzuletas005@gmail.com"
                }
            );
        }
    }
}