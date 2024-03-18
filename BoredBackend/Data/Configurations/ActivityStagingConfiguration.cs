using Bored.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoredBackend.Data.Configurations;

public class ActivityStagingConfiguration : IEntityTypeConfiguration<ActivityStaging>
{
    public void Configure(EntityTypeBuilder<ActivityStaging> builder)
    {
        builder.ToTable("ActivityStaging", "Bored");
        builder.HasKey(o => o.Key);
        builder.Property(o => o.Key)
            .HasMaxLength(300);
        
        builder.Property(o => o.Activity)
            .IsRequired()
            .HasMaxLength(2000);
        
        builder.Property(o => o.Type)
            .IsRequired()
            .HasMaxLength(300);
        
        builder.Property(o => o.Link)
            .IsRequired()
            .HasMaxLength(5000);
    }
}