using BoredBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoredBackend.Data.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers", "Bored");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(o => o.BuyUrl)
            .IsRequired()
            .HasMaxLength(5000);
        
        // builder.HasOne(a => a.Activity)
        //     .WithMany()
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}