using KolibSoft.MsgHub.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Core.Utils;

public static class ModelBuilderUtils
{

    public static void BuildMsgHub(this ModelBuilder builder)
    {
        builder.Entity<MessageModel>(config =>
        {
            config.ToTable("message");
            config.Property(x => x.Id);
            config.Property(x => x.Sender).HasMaxLength(32);
            config.Property(x => x.Receiver).HasMaxLength(32);
            config.Property(x => x.Message).HasMaxLength(256);
            config.Property(x => x.State).HasMaxLength(32);
            config.Property(x => x.UpdatedAt);
            config.HasKey(x => x.Id);
        });
    }

}