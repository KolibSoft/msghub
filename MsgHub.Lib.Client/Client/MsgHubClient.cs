using KolibSoft.MsgHub.Abstractions;

namespace KolibSoft.MsgHub.Client;

public class MsgHubClient : IMsgHubContext
{

    public HttpClient HttpClient { get; }
    public MsgHubUris Uris { get; }

    public IMessageCatalogue MessageCatalogue { get; }

    public MsgHubClient(HttpClient httpClient, MsgHubUris uris)
    {
        HttpClient = httpClient;
        Uris = uris;
        MessageCatalogue = new IMessageCatalogue.WebCatalogue(httpClient, uris.Message);
    }

    public class MsgHubUris
    {
        public string Message { get; }
        public MsgHubUris(string message)
        {
            Message = message;
        }
    }

}