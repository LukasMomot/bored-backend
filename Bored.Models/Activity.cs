namespace Bored.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Participants { get; set; }
    public decimal Price { get; set; }
    public string Link { get; set; }
    public string ExternalKey { get; set; }
    public decimal Accessibility { get; set; }
    
    public virtual List<Offer> Offers { get; set; } = [];
}