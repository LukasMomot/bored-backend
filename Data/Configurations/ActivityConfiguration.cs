using BoredBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoredBackend.Data.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities", "Bored");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.ExternalKey)
            .HasMaxLength(200);
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(a => a.Type)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(a => a.Link)
            .HasMaxLength(5000);

        builder.HasMany<Offer>()
            .WithOne(a => a.Activity)
            .OnDelete(DeleteBehavior.Cascade);
    }
}