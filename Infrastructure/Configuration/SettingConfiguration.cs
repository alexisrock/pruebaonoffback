using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class SettingConfiguration : IEntityTypeConfiguration<Domain.Entities.Configuration>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Configuration> builder)
        {
            builder.ToTable("Configuration");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Value).HasMaxLength(2000);


        }
    }
}
