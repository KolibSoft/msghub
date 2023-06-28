using KolibSoft.MsgHub.Common;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.MsgHub.Utils;

public static class PolicyExtensions
{

    public static void AddMsgHubPolicies(this AuthorizationBuilder builder)
    {
        builder.AddPolicy(PolicyNames.MessageReader, options => options.RequireClaim(MsghClaimTypes.Permission, PermissionCodes.ReadMessage, PermissionCodes.ManageMessage));
        builder.AddPolicy(PolicyNames.MessageManager, options => options.RequireClaim(MsghClaimTypes.Permission, PermissionCodes.ManageMessage));
    }

}