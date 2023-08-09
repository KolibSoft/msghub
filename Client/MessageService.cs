using KolibSoft.Catalogue.Client;
using KolibSoft.Catalogue.Core;
using KolibSoft.MsgHub.Core.Models;

namespace KolibSoft.MsgHub.Client;

public class MessageService : CatalogueService<MessageModel, CatalogueFilters>
{

    public MessageService(HttpClient httpClient, string uri) : base(httpClient, uri) {}

}