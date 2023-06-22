using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Catalogues;
using KolibSoft.MsgHub.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Abstractions;

public interface IMessageCatalogue : ICatalogue<MessageModel, IMessageCatalogue.IFilters>
{

    public interface IFilters { }

    public class MemoryCatalogue : MemoryCatalogue<MessageModel, IFilters>, IMessageCatalogue
    {
        public MemoryCatalogue(ICollection<MessageModel> collection) : base(collection) { }
    }

    public class DataBaseCatalogue : DatabaseCatalogue<MessageModel, IFilters>, IMessageCatalogue
    {
        public DataBaseCatalogue(DbContext dbContext) : base(dbContext) { }
    }

    public class WebCatalogue : WebCatalogue<MessageModel, IFilters>, IMessageCatalogue
    {
        public WebCatalogue(HttpClient httpClient, string uri) : base(httpClient, uri) { }
    }

}