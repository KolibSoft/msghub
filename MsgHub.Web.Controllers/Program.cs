using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// builder.Services.AddSingleton<IMessageCatalogue>(new IMessageCatalogue.MemoryCatalogue(new List<MessageModel>()));

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();