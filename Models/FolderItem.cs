namespace AiWPFDemo.Models;

public sealed class FolderItem
{
    public FolderItem(string name, string fullPath)
    {
        Name = name;
        FullPath = fullPath;
    }

    public string Name { get; }
    public string FullPath { get; }

    public override string ToString() => Name;
}
