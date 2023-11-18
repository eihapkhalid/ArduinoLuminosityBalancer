using ArduinoWebApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ArduinoWebApp.Acsses.Data
{
    public class ArduinoAppDbContext : DbContext
    {
        public ArduinoAppDbContext(DbContextOptions<ArduinoAppDbContext> options)
            : base(options)
        {
        }
        public DbSet<TbLdrSensorReading> TbLdrSensorReadings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

    }
}
