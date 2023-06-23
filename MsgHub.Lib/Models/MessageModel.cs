using System;
using System.ComponentModel.DataAnnotations.Schema;
using KolibSoft.Catalogue.Abstractions;
using KolibSoft.MsgHub.Common;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Models;

[Table("message")]
[PrimaryKey("Id")]
public class MessageModel : IRegister, IUpdatable<MessageModel>
{

    public const string Ready = "Ready";
    public const string Delivering = "Delivering";
    public const string Delivered = "Delivered";

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sender { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string State { get; set; } = Ready;
    public bool Active { get; set; } = false;

    public bool Validate(ICollection<string>? errors = null)
    {
        var valid = true;
        if (string.IsNullOrWhiteSpace(Sender) || Sender.Length > byte.MaxValue)
        {
            errors?.Add(ErrorCodes.InvalidSender);
            valid = false;
        }
        if (string.IsNullOrWhiteSpace(Receiver) || Receiver.Length > byte.MaxValue)
        {
            errors?.Add(ErrorCodes.InvalidReceiver);
            valid = false;
        }
        if (string.IsNullOrWhiteSpace(Content) || Content.Length > ushort.MaxValue)
        {
            errors?.Add(ErrorCodes.InvalidContent);
            valid = false;
        }
        return valid;
    }

    public void Update(MessageModel model)
    {
        Sender = model.Sender;
        Receiver = model.Receiver;
        Content = model.Content;
        State = model.State;
        Active = model.Active;
    }

}