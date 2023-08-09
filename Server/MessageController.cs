using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Server;
using KolibSoft.MsgHub.Core.Models;

namespace KolibSoft.MsgHub.Server;

public class MessageController : CatalogueController<MessageModel, CatalogueFilters>
{

    public MessageController(ICatalogueConnector<MessageModel, CatalogueFilters> catalogueConnector) : base(catalogueConnector) { }

}