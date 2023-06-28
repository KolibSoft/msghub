namespace KolibSoft.MsgHub.Abstractions;

public interface IWebContext
{
    public IMessageClient MessageClient { get; }
}