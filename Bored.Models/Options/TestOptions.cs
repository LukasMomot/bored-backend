namespace Bored.Models.Options;

public class TestOptions
{
    public static string SectionName => "TestOptions";
    public bool Enabled { get; set; }
    public string Url { get; set; } = null!;
}