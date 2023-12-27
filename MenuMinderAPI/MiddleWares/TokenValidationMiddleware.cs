using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using Services.Helpers;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace MenuMinderAPI.MiddleWares
{
    public class TokenValidationMiddleware : IMiddleware
    {
        private readonly JwtServices _jwtService;

        public TokenValidationMiddleware(JwtServices jwtService)
        {
            this._jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Check matching route
            if (!IsRouteToBeHandled(context))
            {
                await next(context);
                return;
            }

            // Extract the token from the request header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                var problem = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Type = "Unauthorized",
                    Detail = "Expected AccessToken in Header of request."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                await next(context);
                return;
            }

            // validate Token
            ResultValidateTokenDto payload = new ResultValidateTokenDto();
            try
            {
                payload = this._jwtService.VerifyToken(token);

                // create list claims
                var claims = new List<Claim>
                {
                    new Claim("AccountId", payload.AccountId),
                    new Claim("Email", payload.Email),
                    new Claim("Role", payload.Role)
                };
                // create claims identity
                var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
                // create claims principle
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                context.User = claimsPrincipal;
                await next(context);
                return;
            }
            catch (Exception)
            {
                await next(context);
            }
        }

        private bool IsRouteToBeHandled(HttpContext context)
        {
            // List path api want to apply middle ware
            var targetRoutes = new Dictionary<string, List<string>>
            {
                { "/api/auth/test-verify-token", new List<string> {"POST" } },
                { "/api/accounts/create", new List <string> { "POST" } },
                { "/api/accounts", new List <string> { "GET", "PUT", "DELETE" } },
                { "/api/me", new List <string> { "GET", "PUT" } },
                { "/api/reservations", new List<string> {"GET", "PUT", "POST" } },
                { "/api/bills", new List<string> {"POST" } },
            };
            // get information of request
            var requestPath = context.Request.Path;
            var requestMethod = context.Request.Method;

            // check paths is matching
            if (targetRoutes.Any(route =>
                requestPath.StartsWithSegments(route.Key) &&
                route.Value.Any(method => string.Equals(requestMethod, method, StringComparison.OrdinalIgnoreCase))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
