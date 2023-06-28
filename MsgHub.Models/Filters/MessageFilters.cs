using KolibSoft.Catalogue.Common;

namespace KolibSoft.MsgHub.Filters;

public class MessageFilters : PageFilters
{
    public string Hint { get; set; } = string.Empty;
    public bool Clean { get; set; } = true;
    public string State { get; set; } = string.Empty;
}