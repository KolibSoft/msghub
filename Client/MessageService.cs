using KolibSoft.Catalogue.Client;
using KolibSoft.Catalogue.Core;
using KolibSoft.MsgHub.Core.Models;
using KolibSoft.MsgHub.Core;

namespace KolibSoft.MsgHub.Client;

public class MessageService : CatalogueService<MessageModel, MessageFilters>
{

    public MessageService(HttpClient httpClient, string uri) : base(httpClient, uri) { }

}