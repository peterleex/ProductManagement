using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public class AccessTokenParser
    {
        private readonly JwtSecurityToken _jwtToken;

        public AccessTokenParser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            _jwtToken = handler.ReadJwtToken(token);
        }

        // �зǪ� Claim
        public string Issuer => GetClaimValue(JwtRegisteredClaimNames.Iss); // �o���
        public string Subject => GetClaimValue(JwtRegisteredClaimNames.Sub); // �D��
        public string Audience => GetClaimValue(JwtRegisteredClaimNames.Aud); // ����
        public DateTime? Expiration => GetDateTimeClaimValue(JwtRegisteredClaimNames.Exp); // ����ɶ�
        public DateTime? NotBefore => GetDateTimeClaimValue(JwtRegisteredClaimNames.Nbf); // �ͮĮɶ�
        public DateTime? IssuedAt => GetDateTimeClaimValue(JwtRegisteredClaimNames.Iat); // ñ�o�ɶ�
        public string JwtId => GetClaimValue(JwtRegisteredClaimNames.Jti); // JWT ID
        public string? Email => GetClaimValue(JwtRegisteredClaimNames.Email); // �l�c
        
        // �۩w�q�� Claim
        public string Role => GetClaimValue("role"); // ����
        public string PreferredUsername => GetClaimValue("preferred_username"); // ����Τ�W
        public string GivenName => GetClaimValue("given_name"); // �W�r

        private string GetClaimValue(string claimType)
        {
            return _jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value!;
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
