using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Catalogues;
using KolibSoft.MsgHub.Models;
using KolibSoft.MsgHub.Utils;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Abstractions;

public interface IMessageCatalogue : ICatalogue<MessageModel, IMessageCatalogue.Filters>
{

    public class Filters
    {
        public string Hint { get; set; } = string.Empty;
        public bool Clean { get; set; } = true;
        public IEnumerable<MessageModel.MessageState> States { get; set; } = new List<MessageModel.MessageState>();
    }

    public class MemoryCatalogue : MemoryCatalogue<MessageModel, Filters>, IMessageCatalogue
    {
        protected override IEnumerable<MessageModel> FilterItems(IEnumerable<MessageModel> items, Filters filters) => items.Filter(filters);
        public MemoryCatalogue(ICollection<MessageModel> collection) : base(collection) { }
    }

    public class DataBaseCatalogue : DatabaseCatalogue<MessageModel, Filters>, IMessageCatalogue
    {
        protected override IEnumerable<MessageModel> FilterItems(IEnumerable<MessageModel> items, Filters filters) => items.Filter(filters);
        public DataBaseCatalogue(DbContext dbContext) : base(dbContext) { }
    }

    public class WebCatalogue : WebCatalogue<MessageModel, Filters>, IMessageCatalogue
    {
        public WebCatalogue(HttpClient httpClient, string uri) : base(httpClient, uri) { }
    }

}