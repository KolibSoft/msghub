using KolibSoft.MsgHub.Abstractions;
using KolibSoft.MsgHub.Models;

namespace KolibSoft.MsgHub.Utils;

public static class FiltersExtensions
{

    public static IEnumerable<MessageModel> Filter(this IEnumerable<MessageModel> messages, IMessageCatalogue.Filters filters)
    {
        if (!string.IsNullOrWhiteSpace(filters.Hint))
        {
            messages = messages.Where(x =>
                x.Sender.Contains(filters.Hint, StringComparison.InvariantCultureIgnoreCase) ||
                x.Receiver.Contains(filters.Hint, StringComparison.InvariantCultureIgnoreCase) ||
                x.Content.Contains(filters.Hint, StringComparison.InvariantCultureIgnoreCase)
            );
        }
        if (filters.Clean) messages = messages.Where(x => x.Active);
        if (filters.States.Count() > 0) messages = messages.Where(x => filters.States.Any(xx => x.State == xx));
        messages = messages.OrderBy(x => x.State).OrderByDescending(x => x.Active);
        return messages;
    }

}