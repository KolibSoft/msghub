using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.AuthStore.Server.Controllers;
using KolibSoft.MsgHub.Core;
using KolibSoft.MsgHub.Core.Utils;
using KolibSoft.MsgHub.Server;
using Microsoft.AspNetCore.Cors;

namespace KolibSoft.AuthStore.Server.Example;

[Route("auth")]
[EnableCors("Allow-All")]
public class TestAuthController : AuthController
{
    public TestAuthController(MsgHubContext context, TokenGenerator generator) : base(new DatabaseAuth(context, generator)) { }
}

[Route("message")]
[EnableCors("Allow-All")]
public class TestMessageController : MessageController
{
    public TestMessageController(MsgHubContext context) : base(new MessageDatabaseCatalogue(context)) { }
}

public class MsgHubContext : DbContext
{

    public DbSet<CredentialModel> Credentials { get; init; } = null!;
    public DbSet<PermissionModel> Permissions { get; init; } = null!;
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildAuthStore();
        modelBuilder.BuildMsgHub();
    }

    public MsgHubContext() : base()
    {
        if (Database.EnsureCreated())
        {
            this.CreateAuthStore();
            this.CreateMsgHub();
        }
    }

    public MsgHubContext(DbContextOptions<MsgHubContext> options) : base(options)
    {
        if (Database.EnsureCreated())
        {
            this.CreateAuthStore();
            this.CreateMsgHub();
        }
    }

}