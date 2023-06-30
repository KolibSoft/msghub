using KolibSoft.Catalogue.Abstractions;
using KolibSoft.MsgHub.Filters;
using KolibSoft.MsgHub.Models;

namespace KolibSoft.MsgHub.Abstractions;

public interface IMessageCatalogue : ISyncCatalogue<MessageModel, MessageFilters>, IAsyncCatalogue<MessageModel, MessageFilters> { }