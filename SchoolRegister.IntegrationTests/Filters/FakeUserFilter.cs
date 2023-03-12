using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SchoolRegister.IntegrationTests.Filters;

public class FakeUserFilter : IAsyncActionFilter
{
    private readonly string _role;

    public FakeUserFilter(string role)
    {
        _role = role;
    }

    /// <summary>
    /// Mocking Claims for User with administrator permissions.
    /// </summary>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var claimsPrincipal = new ClaimsPrincipal();

        // Add needed Claims. 
        claimsPrincipal.AddIdentity(new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, _role),
            }));

        context.HttpContext.User = claimsPrincipal;

        await next();
    }
}
