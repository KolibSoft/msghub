using Microsoft.EntityFrameworkCore;
using KolibSoft.MsgHub.Core.Utils;

namespace KolibSoft.MsgHub.Core;

public class MsgHubContext : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildMsgHub();
    }

    public MsgHubContext() : base() { }
    public MsgHubContext(DbContextOptions<MsgHubContext> options) : base(options) { }

}