using KolibSoft.AuthStore.Client;
using KolibSoft.MsgHub.Client;
using KolibSoft.MsgHub.Models;

var server = "http://localhost:5277";
var httpClient = new HttpClient();

var authClient = new AuthClient(httpClient, $"{server}/auth");
var messageClient = new MsgHubClient(httpClient, new MsgHubClient.MsgHubUris($"{server}/message"));

var auth = await authClient.LoginAsync(new AuthClient.Request
{
    Identity = "MANU",
    Key = "MANU"
});

var errors = new List<string>();
var result = messageClient.MessageCatalogue.CreateItem(new MessageModel
{
    Sender = "CHAVA",
    Receiver = "MANU",
    Content = "HI MANU"
}, errors);

var messages = messageClient.MessageCatalogue.GetPage(0);
var success = true;