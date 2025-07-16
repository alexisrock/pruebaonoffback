using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configuration
{
    public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.ToTable("Tarea");
            builder.HasKey(u => u.IdTarea);
            builder.Property(u => u.NameTarea).IsRequired().HasMaxLength(200);
            builder.Property(u => u.DescriptionTarea).HasMaxLength(4000);
            builder.Property(u => u.IsCompleted);
        }
    }
}
