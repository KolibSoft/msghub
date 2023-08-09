using Microsoft.EntityFrameworkCore;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.MsgHub.Core.Utils;

namespace KolibSoft.MsgHub.Core;

public class MsgHubContext : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildAuthStore();
        modelBuilder.BuildMsgHub();
    }

    public MsgHubContext() : base()
    {
        if (Database.EnsureCreated())
        {
            this.CreateAuthStore();
            this.CreateMsgHub();
        }
    }
    public MsgHubContext(DbContextOptions<MsgHubContext> options) : base(options)
    {
        if (Database.EnsureCreated())
        {
            this.CreateAuthStore();
            this.CreateMsgHub();
        }
    }

}