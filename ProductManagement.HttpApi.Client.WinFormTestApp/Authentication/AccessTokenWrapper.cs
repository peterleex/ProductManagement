using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public class AccessTokenWrapper
    {
        private readonly JwtSecurityToken _jwtToken;

        public AccessTokenWrapper(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            _jwtToken = handler.ReadJwtToken(token);
        }

        public string Issuer => GetClaimValue(JwtRegisteredClaimNames.Iss);
        public string Subject => GetClaimValue(JwtRegisteredClaimNames.Sub);
        public string Audience => GetClaimValue(JwtRegisteredClaimNames.Aud);
        public DateTime? Expiration => GetDateTimeClaimValue(JwtRegisteredClaimNames.Exp);
        public DateTime? NotBefore => GetDateTimeClaimValue(JwtRegisteredClaimNames.Nbf);
        public DateTime? IssuedAt => GetDateTimeClaimValue(JwtRegisteredClaimNames.Iat);
        public string JwtId => GetClaimValue(JwtRegisteredClaimNames.Jti);
        public string PreferredUsername => GetClaimValue("preferred_username");
        public string GivenName => GetClaimValue("given_name");

        private string GetClaimValue(string claimType)
        {
            return _jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

        private DateTime? GetDateTimeClaimValue(string claimType)
        {
            var claimValue = GetClaimValue(claimType);
            if (long.TryParse(claimValue, out long unixTime))
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime;
            }
            return null;
        }
    }
}
