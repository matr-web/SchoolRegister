using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace SchoolRegister.IntegrationTests.Evaluators;

public class FakePolicyEvaluator : IPolicyEvaluator
{ 
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsPrincipal = new ClaimsPrincipal();

        var ticket = new AuthenticationTicket(claimsPrincipal, "Test");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Bypass [Authorize] attribute. It just returns Success as a result.
    /// </summary>
    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        var result = PolicyAuthorizationResult.Success();
        return Task.FromResult(result);
    }
}
