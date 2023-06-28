using KolibSoft.MsgHub.Common;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.MsgHub.Utils;

public static class PolicyExtensions
{

    public static void AddMsgHubPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(PolicyNames.MessageReader, options => options.RequireClaim(MsghClaimTypes.Permission, PermissionCodes.ReadMessage, PermissionCodes.ManageMessage));
        options.AddPolicy(PolicyNames.MessageManager, options => options.RequireClaim(MsghClaimTypes.Permission, PermissionCodes.ManageMessage));
    }

}