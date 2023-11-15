using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApplication1.Other;

public static class Helper
{
    public static (string email, string name) GetTokenData(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();
        string name;
        string email;

        var tokenDescriptor = tokenHandler.ReadJwtToken(token);
        var claims = tokenDescriptor.Claims.ToList();

        var nameClaim = claims.First(claim => claim.Type.Equals(ClaimTypes.Name));
        name = nameClaim.Value;

        var emailClaim = claims.First(claim => claim.Type.Equals(ClaimTypes.Email));
        email = emailClaim.Value;

        return (email, name);
    }
}