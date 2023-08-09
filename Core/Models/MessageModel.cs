using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.MsgHub.Core.Models;

public class MessageModel : IItem, IValidatable, IUpdatable<MessageModel>
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sender { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool Validate(ICollection<string>? errors = default)
    {
        var valid = true;
        if (Sender.Length > 64 || string.IsNullOrWhiteSpace(Sender))
        {
            errors?.Add(MsgHubStatics.InvalidSender);
            valid = false;
        }
        if (Receiver.Length > 64 || string.IsNullOrWhiteSpace(Receiver))
        {
            errors?.Add(MsgHubStatics.InvalidReceiver);
            valid = false;
        }
        if (Message.Length > 256 || string.IsNullOrWhiteSpace(Message))
        {
            errors?.Add(MsgHubStatics.InvalidMessage);
            valid = false;
        }
        if (string.IsNullOrWhiteSpace(State) || (State != MsgHubStatics.Ready && State != MsgHubStatics.Sending && State != MsgHubStatics.Delivered))
        {
            errors?.Add(MsgHubStatics.InvalidState);
            valid = false;
        }
        return valid;
    }

    public void Update(MessageModel newState)
    {
        Sender = newState.Sender;
        Receiver = newState.Receiver;
        Message = newState.Message;
        State = newState.State;
        UpdatedAt = DateTime.UtcNow;
    }

}