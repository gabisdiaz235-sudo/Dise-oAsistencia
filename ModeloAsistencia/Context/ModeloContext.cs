using Microsoft.EntityFrameworkCore;
using ModeloAsistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModeloAsistencia.Context
{
    public class ModeloContext : DbContext
    {
        public string conexion = @"Server=DESKTOP-I7VPMSN\SQLEXPRESS;Database=AsistenciaEmpleados;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Administrador> Administradors { get; set; }
        public DbSet<RegistroEntradaSalida> RegistroEntradaSalidas { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Pais> Pais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
            @"Server=DESKTOP-I7VPMSN\SQLEXPRESS;Database=AsistenciaEmpleados;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

        }

        
        public string MiString
        {
            get { return conexion; }
        }

    }
}
