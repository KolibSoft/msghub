using KolibSoft.AuthStore.Models;
using KolibSoft.AuthStore.Utils;
using KolibSoft.Jwt.Extensions;
using KolibSoft.MsgHub.Models;
using KolibSoft.MsgHub.Utils;
using Microsoft.EntityFrameworkCore;

var connstring = "server=localhost;user=root;password=root;database=kolib_database;";
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


public class Context : DbContext, KolibSoft.AuthStore.Abstractions.IDatabaseContext, KolibSoft.MsgHub.Abstractions.IDatabaseContext
{

    public DbSet<CredentialModel> Credentials { get; set; } = null!;
    public DbSet<PermissionModel> Permissions { get; set; } = null!;
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; set; } = null!;

    public DbSet<MessageModel> Messages { get; set; } = null!;

    public Context() : base() { }
    public Context(DbContextOptions options) : base(options) { }

}