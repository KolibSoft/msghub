using KolibSoft.AuthStore.Abstractions;
using KolibSoft.AuthStore.Controllers;
using KolibSoft.AuthStore.Models;
using KolibSoft.AuthStore.Policies;
using KolibSoft.Jwt.Extensions;
using KolibSoft.Jwt.Services;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Controllers;
using KolibSoft.MsgHub.Models;
using KolibSoft.MsgHub.Policies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var connstring = "server=localhost;user=root;password=root;database=kolib_database";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMySql<Context>(connstring, ServerVersion.AutoDetect(connstring));
builder.Services.AddJwt("JWT_SECRET_PHRASE");
builder.Services.AddAuthorization(options =>
{
    options.AddAuthStorePolicies();
    options.AddMsgHubPolicies();
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

[Route("auth")]
public class TestAuthController : AuthController
{
    public TestAuthController(Context context, JwtGenerator generator) : base(context, generator) { }
}

[Route("message")]
public class TestMessageController : MessageController
{
    public TestMessageController(Context context) : base(context) { }
}

public class Context : DbContext,
    IAuthStoreContext,
    ICredentialCatalogue.DataBaseCatalogue<Context>.IContext,
    IPermissionCatalogue.DataBaseCatalogue<Context>.IContext,
    ICredentialPermissionCatalogue.DataBaseCatalogue<Context>.IContext,
    IMsgHubContext,
    IMessageCatalogue.DataBaseCatalogue<Context>.IContext
{

    public DbSet<CredentialModel> Credentials { get; set; } = null!;
    public DbSet<PermissionModel> Permissions { get; set; } = null!;
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; set; } = null!;
    public DbSet<MessageModel> Messages { get; set; } = null!;

    public ICredentialCatalogue CredentialCatalogue { get; }
    public IPermissionCatalogue PermissionCatalogue { get; }
    public ICredentialPermissionCatalogue CredentialPermissionCatalogue { get; }
    public IMessageCatalogue MessageCatalogue { get; }

    IEnumerable<CredentialPermissionModel> ICredentialCatalogue.IContext.CredentialPermissions => CredentialPermissions;
    IEnumerable<CredentialPermissionModel> IPermissionCatalogue.IContext.CredentialPermissions => CredentialPermissions;

    public Context(DbContextOptions<Context> options) : base(options)
    {
        CredentialCatalogue = new ICredentialCatalogue.DataBaseCatalogue<Context>(this);
        PermissionCatalogue = new IPermissionCatalogue.DataBaseCatalogue<Context>(this);
        CredentialPermissionCatalogue = new ICredentialPermissionCatalogue.DataBaseCatalogue<Context>(this);
        MessageCatalogue = new IMessageCatalogue.DataBaseCatalogue<Context>(this);
    }

}