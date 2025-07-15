using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PruebaContext : DbContext
    {
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options) { }
        public virtual DbSet<Tarea>? Tarea { get; set; }
        public virtual DbSet<Usuario>? Usuario { get; set; }
        public virtual DbSet<Rol>? Rol { get; set; }
        public virtual DbSet<AsignarTarea>? AsignarTarea { get; set; }
        public virtual DbSet<Configuration>? Configuraciones { get; set; }
        public virtual DbSet<AsignarTareaIdUsuario>? AsignarTareaIdUsuario { get; set; }
        public virtual DbSet<TareaAsignadas>? TareaAsignadas { get; set; }
        public virtual DbSet<TareasSinAsignar>? TareasSinAsignar { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsignarTareaIdUsuario>().HasNoKey().ToView("SPGetAsignacionTareasByIdUsuario");
            modelBuilder.Entity<TareaAsignadas>().HasNoKey().ToView("SPGetAsignacionTareas");
            modelBuilder.Entity<TareasSinAsignar>().HasNoKey().ToView("SPGettareaSinAsignacion");


        }








    }
}
