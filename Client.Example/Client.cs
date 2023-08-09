using Microsoft.EntityFrameworkCore;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Core;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.MsgHub.Core.Utils;
using KolibSoft.MsgHub.Core.Models;
using KolibSoft.MsgHub.Core;
using KolibSoft.MsgHub.Client;

namespace KolibSoft.AuthStore.Client.Example;

public class MsgHubClient
{

    public HttpClient HttpClient { get; } = new HttpClient();
    public string Uri { get; }
    public DbContext DbContext { get; }
    public MsgHubChanges Changes { get; }

    public IAuthConnector Auth { get; }
    public ServiceCatalogue<MessageModel, CatalogueFilters> Messages { get; }

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
        Messages = new ServiceCatalogue<MessageModel, CatalogueFilters>(
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