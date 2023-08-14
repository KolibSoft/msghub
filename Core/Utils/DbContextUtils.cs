using KolibSoft.AuthStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.MsgHub.Core.Utils;

public static class DbContextUtils
{

    public static void CreateMsgHub(this DbContext dbContext)
    {
        var credentials = dbContext.Set<CredentialModel>();
        var permissions = dbContext.Set<PermissionModel>();
        var credentialPermissions = dbContext.Set<CredentialPermissionModel>();
        var rootCredential = credentials.FirstOrDefault(x => x.Identity == "ROOT")!;
        var msgHubPermissions = new PermissionModel[] {
            new PermissionModel {
                Id = Guid.NewGuid(),
                Code = MsgHubStatics.ReadMessage,
                Active = true,
                UpdatedAt = DateTime.UtcNow
            },
            new PermissionModel {
                Id = Guid.NewGuid(),
                Code = MsgHubStatics.ManageMessage,
                Active = true,
                UpdatedAt = DateTime.UtcNow
            }
        };
        permissions.AddRange(msgHubPermissions);
        var rootPermissions = msgHubPermissions.Select(x => new CredentialPermissionModel
        {
            Id = Guid.NewGuid(),
            CredentialId = rootCredential.Id,
            PermissionId = x.Id,
            Active = true,
            UpdatedAt = DateTime.UtcNow
        }).ToArray();
        credentialPermissions.AddRange(rootPermissions);
        dbContext.SaveChanges();
    }

}