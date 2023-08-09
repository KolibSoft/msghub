using KolibSoft.MsgHub.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Core;

public class MessageDatabaseCatalogue : DatabaseCatalogue<MessageModel, MessageFilters>
{

    protected override Task<IQueryable<MessageModel>> QueryItems(IQueryable<MessageModel> items, MessageFilters? filters = null) => Task.Run(() =>
    {
        if (filters?.State != null) items = items.Where(x => x.State == filters.State);
        if (filters?.Hint != null) items = items.Where(x => EF.Functions.Like(x.Sender, $"%{filters.Hint}%") || EF.Functions.Like(x.Receiver, $"%{filters.Hint}%") || EF.Functions.Like(x.Content, $"%{filters.Hint}%"));
        items = items.OrderBy(x => x.UpdatedAt).OrderBy(x => x.State);
        return items;
    });

    public MessageDatabaseCatalogue(DbContext dbContext) : base(dbContext) { }

}