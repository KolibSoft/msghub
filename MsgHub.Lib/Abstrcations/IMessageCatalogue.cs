using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Catalogues;
using KolibSoft.MsgHub.Models;
using KolibSoft.MsgHub.Utils;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Abstractions;

public interface IMessageCatalogue : ICatalogue<MessageModel, IMessageCatalogue.Filters>
{

    public interface IContext { }

    public class Filters : IPageFilters
    {
        public string Hint { get; set; } = string.Empty;
        public bool Clean { get; set; } = true;
        public IEnumerable<string> States { get; set; } = new List<string>();
        public int PageIndex { get; set; } = 0;
    }

    public class MemoryCatalogue<TContext> : MemoryCatalogue<MessageModel, Filters>, IMessageCatalogue
        where TContext : MemoryCatalogue<TContext>.IContext
    {

        public IContext Context { get; }

        protected override IEnumerable<MessageModel> FilterItems(IEnumerable<MessageModel> items, Filters filters) => items.Filter(filters);

        public MemoryCatalogue(TContext context) : base(context.Messages)
        {
            Context = context;
        }

        public interface IContext : IMessageCatalogue.IContext
        {
            public ICollection<MessageModel> Messages { get; }
        }

    }

    public class DataBaseCatalogue<TContext> : DatabaseCatalogue<MessageModel, Filters>, IMessageCatalogue
        where TContext : DbContext, DataBaseCatalogue<TContext>.IContext
    {

        public IContext Context { get; }

        protected override IEnumerable<MessageModel> FilterItems(IEnumerable<MessageModel> items, Filters filters) => items.Filter(filters);

        public DataBaseCatalogue(TContext context) : base(context)
        {
            Context = context;
        }

        public interface IContext : IMessageCatalogue.IContext
        {
            public DbSet<MessageModel> Messages { get; }
        }

    }

    public class WebCatalogue : WebCatalogue<MessageModel, Filters>, IMessageCatalogue
    {
        public WebCatalogue(HttpClient httpClient, string uri) : base(httpClient, uri) { }
    }

}