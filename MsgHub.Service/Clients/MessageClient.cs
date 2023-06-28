using KolibSoft.Catalogue.Clients;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Filters;
using KolibSoft.MsgHub.Models;

namespace KolibSoft.MsgHub.Clients;

public class MessageClient : CatalogueClient<MessageModel, MessageFilters>, IMessageClient
{

    public MessageClient(HttpClient httpClient, string uri) : base(httpClient, uri) {}
    
}