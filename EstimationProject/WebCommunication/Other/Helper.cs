using System.IdentityModel.Tokens.Jwt;

namespace WebApplication1.Other;

public class Helper
{
    public static (string email, string name) GetTokenData(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        // Console.WriteLine(token);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        string name;
        string email;
        
        
        // Console.WriteLine("Before Token Descriptor");
        var tokenDescriptor = tokenHandler.ReadJwtToken(token);
        var claims = tokenDescriptor.Claims.ToList();
        // Console.WriteLine("After getting claims");

        var nameClaim = claims.First(claim => claim.Type.Contains("name"));
        name = nameClaim.Value;
        // Console.WriteLine("After getting name claim");
            
        // Console.WriteLine(name);

        var emailClaim = claims.First(claim => claim.Type.Contains("emailaddress"));
        email = emailClaim.Value;
        // Console.WriteLine("After getting email claim");
            
        // Console.WriteLine(email);

        return (email, name);
    }
}