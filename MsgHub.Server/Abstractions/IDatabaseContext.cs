using KolibSoft.MsgHub.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Abstractions;

public interface IDatabaseContext
{
    public DbSet<MessageModel> Messages { get; }
}