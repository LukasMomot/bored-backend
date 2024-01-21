using System.Text.Json.Serialization;

namespace BoredBackend.Models;

public class Offer
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string BuyUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [JsonIgnore]
    public virtual Activity Activity { get; set; } = null!;

    public int ActivityId { get; set; }
}