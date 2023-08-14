using KolibSoft.AuthStore.Core;
using KolibSoft.MsgHub.Core;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.MsgHub.Server.Utils;

public static class PolicyUtils
{

    public static void AddMsgHub(this AuthorizationOptions options)
    {
        options.AddPolicy(MsgHubStatics.MessageReader, config => config.RequireClaim(AuthStoreStatics.Permissions, MsgHubStatics.ReadMessage, MsgHubStatics.ManageMessage));
        options.AddPolicy(MsgHubStatics.MessageManager, config => config.RequireClaim(AuthStoreStatics.Permissions, MsgHubStatics.ManageMessage));
    }

}