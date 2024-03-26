namespace Bored.Models.Messages;

public record StartFakeStaging()
{
    public int ProcessId { get; init; }
    public bool StartImmediately { get; init; }
}