namespace AiWPFDemo.Models;

public sealed class WorkItem
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
    public string Drawing { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public string IssueRaw { get; init; } = string.Empty;
}
