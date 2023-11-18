using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebCommunication.Contracts.Other;

namespace WebApplication1.Other;

public static class Helper
{
    public static TokenData GetTokenData(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = tokenHandler.ReadJwtToken(token);
        var claims = tokenDescriptor.Claims.ToList();

        var nameClaim = claims.First(claim => claim.Type.Equals(ClaimTypes.Name));
        var name = nameClaim.Value;

        var emailClaim = claims.First(claim => claim.Type.Equals(ClaimTypes.Email));
        var email = emailClaim.Value;

        return new TokenData(email, name);
    }
}