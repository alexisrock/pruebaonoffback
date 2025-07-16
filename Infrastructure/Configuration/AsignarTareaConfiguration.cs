using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class AsignarTareaConfiguration : IEntityTypeConfiguration<AsignarTarea>
    {
        public void Configure(EntityTypeBuilder<AsignarTarea> builder)
        {
            builder.ToTable("AsignarTarea");
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.Usuario)
             .WithMany()
             .HasForeignKey(u => u.IdUsuario)
             .HasConstraintName("fkAsignarTarea_IdUsuario")
             .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Tarea)
             .WithMany()
             .HasForeignKey(u => u.IdTarea)
             .HasConstraintName("fkAsignarTarea_IdTarea")
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
