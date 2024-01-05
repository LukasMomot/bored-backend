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

        builder.Property(a => a.IsActive)
            .HasDefaultValue(true);
        
        // Optional since this is configured in ActivityConfiguration
        // builder.HasOne(a => a.Activity)
        //     .WithMany(a => a.Offers)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Offer
            {
                Id = 1,
                Name = "Karaoke Bar",
                BuyUrl = "https://www.karaokebar.com",
                ActivityId = 1
            },
            new Offer
            {
                Id = 2,
                Name = "Karaoke Bar 2",
                BuyUrl = "https://www.karaokebar2.com",
                ActivityId = 1
            },
            new Offer
            {
                Id = 3,
                Name = "Tennis Club",
                BuyUrl = "https://www.tennisclub.com",
                ActivityId = 2
            },
            new Offer
            {
                Id = 4,
                Name = "Tennis Club 2",
                BuyUrl = "https://www.tennisclub2.com",
                ActivityId = 2
            }, new Offer
            {
                Id = 5,
                Name = "Animal Shelter",
                BuyUrl = "https://www.animalshelter.com",
                ActivityId = 3
            },
            new Offer
            {
                Id = 6,
                Name = "Animal Shelter 2",
                BuyUrl = "https://www.animalshelter2.com",
                ActivityId = 3
            });
    }
}