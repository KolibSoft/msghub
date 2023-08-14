using KolibSoft.AuthStore.Client.Example;
using KolibSoft.AuthStore.Client.Utils;
using KolibSoft.MsgHub.Core;
using System.Text.Json;

var uri = "http://localhost:5264";
var context = new MsgHubContext();
var changes = File.Exists("changes.json") ? JsonSerializer.Deserialize<MsgHubChanges>(File.ReadAllText("changes.json")) ?? new() : new();

var client = new MsgHubClient(uri, context, changes);
if (client.Auth.Available)
    try
    {
        var auth = await client.Auth.AccessAsync(new()
        {
            Identity = "ROOT",
            Key = "ROOT"
        });
        client.HttpClient.UseToken(auth.Data!.AccessToken);
        await client.Sync();
    }
    catch { }

var page = await client.Messages.PageAsync();

/*
var insert = await client.Messages.InsertAsync(new()
{
    Sender = "ME",
    Receiver = "YOU",
    Message = "HELLO"
});
*/

File.WriteAllText("changes.json", JsonSerializer.Serialize(changes));
_ = 0;