using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Server;
using KolibSoft.MsgHub.Core;
using KolibSoft.MsgHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.MsgHub.Server;

public class MessageController : CatalogueController<MessageModel, MessageFilters>
{

    [Authorize(MsgHubStatics.MessageReader)]
    public override Task<Result<Page<MessageModel>?>> PageAsync([FromQuery] MessageFilters? filters = null)
    {
        return base.PageAsync(filters);
    }

    [Authorize(MsgHubStatics.MessageReader)]
    public override Task<Result<MessageModel?>> GetAsync([FromRoute] Guid id)
    {
        return base.GetAsync(id);
    }

    [Authorize(MsgHubStatics.MessageManager)]
    public override Task<Result<Guid?>> InsertAsync([FromBody] MessageModel item)
    {
        item.Sender = item.Sender.Trim();
        item.Receiver = item.Receiver.Trim();
        item.Content = item.Content.Trim();
        item.State = item.State.Trim();
        return base.InsertAsync(item);
    }

    [Authorize(MsgHubStatics.MessageManager)]
    public override Task<Result<bool?>> UpdateAsync([FromRoute] Guid id, [FromBody] MessageModel item)
    {
        item.Sender = item.Sender.Trim();
        item.Receiver = item.Receiver.Trim();
        item.Content = item.Content.Trim();
        item.State = item.State.Trim();
        return base.UpdateAsync(id, item);
    }

    [Authorize(MsgHubStatics.MessageManager)]
    public override Task<Result<bool?>> DeleteAsync([FromRoute] Guid id)
    {
        return base.DeleteAsync(id);
    }

    public MessageController(ICatalogueConnector<MessageModel, MessageFilters> catalogueConnector) : base(catalogueConnector) { }

}