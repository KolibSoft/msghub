using KolibSoft.Catalogue.Core;

namespace KolibSoft.MsgHub.Core;

public class MessageFilters : CatalogueFilters
{
    public string? State { get; init; }
}