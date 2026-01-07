using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AiWPFDemo.Models;

namespace AiWPFDemo.Services;

public sealed class WorkListService
{
    public IReadOnlyList<WorkItem> LoadWorkItems(string xmlPath)
    {
        var document = XDocument.Load(xmlPath);
        var items = document.Descendants("WorkItem")
            .Select(item => new WorkItem
            {
                Code = (string?)item.Element("Code") ?? string.Empty,
                Name = (string?)item.Element("Name") ?? string.Empty,
                Image = (string?)item.Element("Image") ?? string.Empty,
                Drawing = (string?)item.Element("Drawing") ?? string.Empty,
                Model = (string?)item.Element("Model") ?? string.Empty,
                IssueRaw = (string?)item.Element("Issue") ?? string.Empty
            })
            .ToList();

        return items;
    }
}
