using KolibSoft.MsgHub.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Core;

public class MessageDatabaseCatalogue : DatabaseCatalogue<MessageModel, CatalogueFilters>
{

    public MessageDatabaseCatalogue(DbContext dbContext) : base(dbContext) { }

}