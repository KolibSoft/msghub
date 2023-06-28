using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Controllers;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Filters;
using KolibSoft.MsgHub.Models;

namespace KolibSoft.MsgHub.Controllers;

public abstract class MessageController : CatalogueController<MessageModel, MessageFilters>, IMessageController
{
    protected MessageController(ICatalogue<MessageModel, MessageFilters> catalogue) : base(catalogue) { }

}