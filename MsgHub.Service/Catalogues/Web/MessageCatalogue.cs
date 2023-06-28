using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Catalogues;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Filters;
using KolibSoft.MsgHub.Models;

namespace KolibSoft.MsgHub.Catalogues.Web;

public class MessageCatalogue : WebCatalogue<MessageModel, MessageFilters>, IMessageCatalogue
{

    public MessageCatalogue(ICatalogueClient<MessageModel, MessageFilters> client) : base(client) {}

}