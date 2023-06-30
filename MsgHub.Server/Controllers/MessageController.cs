using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Controllers;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Common;
using KolibSoft.MsgHub.Filters;
using KolibSoft.MsgHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.MsgHub.Controllers;

public abstract class MessageController : CatalogueController<MessageModel, MessageFilters>, IMessageController
{

    [Authorize(PolicyNames.MessageReader)]
    public override Task<IActionResult> GetPageAsync([FromQuery] MessageFilters? filters = null) => base.GetPageAsync(filters);

    [Authorize(PolicyNames.MessageReader)]
    public override Task<IActionResult> GetItemAsync([FromRoute] Guid id) => base.GetItemAsync(id);

    [Authorize(PolicyNames.MessageManager)]
    public override Task<IActionResult> CreateAsync([FromBody] MessageModel item) => base.CreateAsync(item);

    [Authorize(PolicyNames.MessageManager)]
    public override Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] MessageModel item) => base.UpdateAsync(id, item);

    [Authorize(PolicyNames.MessageManager)]
    public override Task<IActionResult> DeleteAsync([FromRoute] Guid id) => base.DeleteAsync(id);

    protected MessageController(IAsyncCatalogue<MessageModel, MessageFilters> catalogue) : base(catalogue) { }

}