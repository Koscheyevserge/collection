using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Api.Core;

namespace Api.Controllers.Auth
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly RoleManager<UserRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthController(RoleManager<UserRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [HttpGet("token")]
        public async System.Threading.Tasks.Task Token()
        {
            var user = await _userManager.FindByEmailAsync("koscheyevserge4@gmail.com");
            if (user == null)
            {
                user = new User { Email = "koscheyevserge4@gmail.com", UserName = "koscheyevserge" };
                var result = await _userManager.CreateAsync(user, "111111");
                if (!result.Succeeded)
                    return;
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var identity = GetIdentity(user, claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(User user, IEnumerable<string> roles)
        {
            var claims = roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)).ToList();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName));
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}