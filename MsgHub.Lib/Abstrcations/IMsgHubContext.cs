namespace KolibSoft.MsgHub.Abstractions;

public interface IMsgHubContext
{
    public IMessageCatalogue MessageCatalogue { get; }
}