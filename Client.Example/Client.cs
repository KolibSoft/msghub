using Microsoft.EntityFrameworkCore;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.MsgHub.Core.Utils;
using KolibSoft.MsgHub.Core;
using KolibSoft.MsgHub.Client;
using KolibSoft.Catalogue.Core.Catalogues;
using KolibSoft.AuthStore.Client.Services;

namespace KolibSoft.AuthStore.Client.Example;

public class MsgHubClient
{

    public HttpClient HttpClient { get; } = new HttpClient();
    public string Uri { get; }
    public DbContext DbContext { get; }
    public MsgHubChanges Changes { get; }

    public IAuthConnector Auth { get; }
    public ServiceCatalogue<MessageModel, MessageFilters> Messages { get; }

    public async Task Sync()
    {
        await Messages.Sync();
    }

    public MsgHubClient(string uri, DbContext dbContext, MsgHubChanges changes)
    {
        Uri = uri;
        DbContext = new MsgHubContext();
        Auth = new AuthService(HttpClient, $"{uri}/auth");
        Changes = changes;
        Messages = new ServiceCatalogue<MessageModel, MessageFilters>(
            new MessageDatabaseCatalogue(dbContext),
            new MessageService(HttpClient, $"{Uri}/message"),
            changes.Messages
        );
    }

}

public class MsgHubChanges
{
    public Dictionary<Guid, string[]?> Messages { get; init; } = new();
}

public class MsgHubContext : DbContext
{

    public DbSet<MessageModel> Messages { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildMsgHub();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    public MsgHubContext() : base() { Database.EnsureCreated(); }
    public MsgHubContext(DbContextOptions<MsgHubContext> options) : base(options) { Database.EnsureCreated(); }

}