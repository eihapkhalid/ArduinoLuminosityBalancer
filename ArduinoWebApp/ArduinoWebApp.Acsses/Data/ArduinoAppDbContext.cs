﻿using ArduinoWebApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
