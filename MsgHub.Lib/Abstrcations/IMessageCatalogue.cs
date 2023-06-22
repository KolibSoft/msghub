using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Catalogues;
using KolibSoft.MsgHub.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Abstractions;

public interface IMessageCatalogue : ICatalogue<MessageModel, IMessageCatalogue.IFilters>
{

    public interface IFilters { }

    public class MemoryCatalogue : MemoryCatalogue<MessageModel, IFilters>
    {
        public MemoryCatalogue(ICollection<MessageModel> collection) : base(collection) { }
    }

    public class DataBaseCatalogue : DatabaseCatalogue<MessageModel, IFilters>
    {
        public DataBaseCatalogue(DbContext dbContext) : base(dbContext) { }
    }

    public class WebCatalogue : WebCatalogue<MessageModel, IFilters>
    {
        public WebCatalogue(HttpClient httpClient, string uri) : base(httpClient, uri) { }
    }

}