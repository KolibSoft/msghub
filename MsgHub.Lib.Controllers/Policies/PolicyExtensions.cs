using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.MsgHub.Policies;

public static class PolicyExtensions {

    public static void AddMsgHubPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(PolicyNames.MessageReader, config => config.RequireClaim(MsgHubClaimTypes.Permission, PermissionCodes.ReadMessage, PermissionCodes.ManageMessage));
        options.AddPolicy(PolicyNames.MessageManager, config => config.RequireClaim(MsgHubClaimTypes.Permission, PermissionCodes.ManageMessage));
    }

}