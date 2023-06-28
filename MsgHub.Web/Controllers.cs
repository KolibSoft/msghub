using KolibSoft.AuthStore.Controllers;
using KolibSoft.Jwt.Services;
using KolibSoft.MsgHub.Catalogues.Database;
using KolibSoft.MsgHub.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class TestAuthController : AuthController
{
    public TestAuthController(Context context, JwtGenerator generator) : base(context, generator) { }
}

[Route("message")]
public class TestMessageController : MessageController
{
    public TestMessageController(Context context) : base(new MessageCatalogue<Context>(context)) { }
}
