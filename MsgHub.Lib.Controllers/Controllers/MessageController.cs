using KolibSoft.Catalogue.Controllers;
using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Models;
using KolibSoft.MsgHub.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.MsgHub.Controllers;

public abstract class MessageController : CatalogueController<MessageModel, IMessageCatalogue.Filters>
{


    public override IActionResult Get([FromQuery] IMessageCatalogue.Filters? filters = null) => NotFound();

    [Authorize(PolicyNames.MessageReader)]
    public override IActionResult GetPage([FromQuery] int pageIndex, [FromQuery] int pageSize = 10, [FromQuery] IMessageCatalogue.Filters? filters = null) => base.GetPage(pageIndex, pageSize, filters);

    [Authorize(PolicyNames.MessageReader)]
    public override IActionResult Get([FromRoute] Guid id) => base.Get(id);

    [Authorize(PolicyNames.MessageManager)]
    public override IActionResult Post([FromBody] MessageModel item) => base.Post(item);

    [Authorize(PolicyNames.MessageManager)]
    public override IActionResult Put([FromRoute] Guid id, [FromBody] MessageModel item) => base.Put(id, item);

    [Authorize(PolicyNames.MessageManager)]
    public override IActionResult Delete([FromRoute] Guid id) => base.Delete(id);

    public MessageController(IMsgHubContext context) : base(context.MessageCatalogue) { }

}