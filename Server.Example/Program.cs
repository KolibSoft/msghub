using System.Text;
using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.AuthStore.Server.Utils;
using KolibSoft.MsgHub.Server.Utils;
using KolibSoft.AuthStore.Server.Example;
using Microsoft.EntityFrameworkCore;

var secret = Encoding.UTF8.GetBytes("SECRET".GetHashString());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("Allow-All", options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
builder.Services.AddDbContext<MsgHubContext>(options =>
{
    var connstring = "server=localhost;user=root;password=root;database=msghub;";
    options.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
});
builder.Services.AddJwtBearer(secret);
builder.Services.AddAuthorization(options =>
{
    options.AddAuthStore();
    options.AddMsgHub();
});
builder.Services.AddSingleton(new TokenGenerator(secret));

var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

