using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Catalogues;
using KolibSoft.Catalogue.Common;
using KolibSoft.Catalogue.Utils;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Filters;
using KolibSoft.MsgHub.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Catalogues.Database;

public class MessageCatalogue<TContext> : SyncCatalogue<MessageModel, MessageFilters>, IMessageCatalogue
    where TContext : DbContext, IDatabaseContext
{

    public TContext Context { get; }

    public override IPageResult<MessageModel> GetPage(MessageFilters? filters = null)
    {
        IEnumerable<MessageModel> items = Context.Messages;
        if (filters != null)
        {
            if (filters.Clean) items = items.Where(x => x.Active);
            if (!string.IsNullOrWhiteSpace(filters.State)) items = items.Where(x => x.State == filters.State);
            if (!string.IsNullOrWhiteSpace(filters.Hint)) items = items.Where(x =>
                x.Sender.Contains(filters.Hint, StringComparison.InvariantCultureIgnoreCase) ||
                x.Receiver.Contains(filters.Hint, StringComparison.InvariantCultureIgnoreCase) ||
                x.Content.Contains(filters.Hint, StringComparison.InvariantCultureIgnoreCase)
            );
        }
        items = items.OrderBy(x => x.State).OrderByDescending(x => x.Active);
        var page = items.GetPage(filters?.PageIndex ?? 0, filters?.PageSize ?? IPageFilters.DefaultPageSize);
        return page;
    }

    public override MessageModel? GetItem(Guid id)
    {
        var item = Context.Messages.FirstOrDefault(x => x.Id == id);
        return item;
    }

    public override Guid Create(MessageModel item, ICollection<string>? errors = null)
    {
        item.Id = Guid.NewGuid();
        if (!item.Validate(errors)) return Guid.Empty;
        Context.Messages.Add(item);
        Context.SaveChanges();
        return item.Id;
    }

    public override bool Update(Guid id, MessageModel item, ICollection<string>? errors = null)
    {
        item.Id = id;
        var original = GetItem(id);
        if (original == null)
        {
            errors?.Add(CatalogueErrorCodes.NoItem);
            return false;
        }
        item.Overlap(original);
        if (!item.Validate(errors)) return false;
        original.Update(item);
        Context.Messages.Update(original);
        Context.SaveChanges();
        return true;
    }

    public override bool Delete(Guid id, ICollection<string>? errors = null)
    {
        var original = GetItem(id);
        if (original == null)
        {
            errors?.Add(CatalogueErrorCodes.NoItem);
            return false;
        }
        original.Active = false;
        Context.Messages.Update(original);
        Context.SaveChanges();
        return true;
    }

    public MessageCatalogue(TContext context)
    {
        Context = context;
    }

}