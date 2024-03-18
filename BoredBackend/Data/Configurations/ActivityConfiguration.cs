using Bored.Models;
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

        // builder.HasMany<Offer>()
        //     .WithOne(a => a.Activity)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Offers)
            .WithOne(o => o.Activity)
            .HasForeignKey(o => o.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(
            new Activity
            {
                Id = 1,
                Name = "Go to a karaoke bar with some friends",
                Type = "social", 
                Participants = 4,
                Price = 0.5m,
                Link = "",
                ExternalKey = "9072906",
                Accessibility = 0.35m,
            },
            new Activity
            {
                Id = 2,
                Name = "Play a game of tennis with a friend",
                Type = "social", Participants = 2,
                Price = 0.1m,
                Link = "",
                ExternalKey = "1093640",
                Accessibility = 0.4m,
            },
            new Activity
            {
                Id = 3,
                Name = "Volunteer at a local animal shelter",
                Type = "charity", Participants = 1,
                Price = 0.1m,
                Link = "",
                ExternalKey = "1382389",
                Accessibility = 0.5m
            }
        );
    }
}