using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Ident.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User = ExpensiFlow.Ident.Models.User;

namespace ExpensiFlow.Ident.Jwt;

internal class JwtGenerator(IOptions<JwtOptions> jwtOptions) : IJwtGenerator
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string Generate(User user, IEnumerable<string> permissions)
    {
        var expiration = DateTime.UtcNow.AddSeconds(_jwtOptions.ExpirationSeconds);

        var token = CreateJwtToken(
            CreateClaims(user, permissions),
            CreateSigningCredentials(),
            expiration
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
        new(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private static Claim[] CreateClaims(IdentityUser<AccountId> user, IEnumerable<string> permissions)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
        };

        var roleClaims = permissions.Select(permission => new Claim("role", permission));
        claims.AddRange(roleClaims);
        return claims.ToArray();
    }

    private SigningCredentials CreateSigningCredentials() =>
        new(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)),
            SecurityAlgorithms.HmacSha256
        );
}