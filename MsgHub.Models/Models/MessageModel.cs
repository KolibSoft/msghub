using KolibSoft.Catalogue.Abstractions;
using KolibSoft.Catalogue.Models;

namespace KolibSoft.MsgHub.Models;

public class MessageModel : AuditableModel, IUpdatable<MessageModel>
{

    public const string Ready = "Ready";
    public const string Delivering = "Delivering";
    public const string Delivered = "Delivered";

    public string Sender { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

    public override bool Validate(ICollection<string>? errors = null)
    {
        var invalid = !base.Validate(errors);
        if (invalid = (string.IsNullOrWhiteSpace(Sender) || Sender.Length > byte.MaxValue)) errors?.Add("INVALID_SENDER");
        if (invalid = (string.IsNullOrWhiteSpace(Receiver) || Receiver.Length > byte.MaxValue)) errors?.Add("INVALID_RECEIVER");
        if (invalid = (string.IsNullOrWhiteSpace(Content) || Content.Length > ushort.MaxValue)) errors?.Add("INVALID_CONTENT");
        if (invalid = (State != Ready && State != Delivering && State != Delivered)) errors?.Add("INVALID_STATE");
        return !invalid;
    }

    public void Overlap(MessageModel model)
    {
        if (string.IsNullOrWhiteSpace(Sender)) Sender = model.Sender;
        if (string.IsNullOrWhiteSpace(Receiver)) Receiver = model.Receiver;
        if (string.IsNullOrWhiteSpace(Content)) Content = model.Content;
        if (string.IsNullOrWhiteSpace(State)) State = model.State;
    }

    public void Update(MessageModel model)
    {
        base.Update(model);
        Sender = model.Sender;
        Receiver = model.Receiver;
        Content = model.Content;
        State = model.State;
        Active = model.Active;
    }

}