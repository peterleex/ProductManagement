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

        // 標準的 Claim
        public string Issuer => GetClaimValue(JwtRegisteredClaimNames.Iss); // 發行者
        public string Subject => GetClaimValue(JwtRegisteredClaimNames.Sub); // 主體
        public string Audience => GetClaimValue(JwtRegisteredClaimNames.Aud); // 受眾
        public DateTime? Expiration => GetDateTimeClaimValue(JwtRegisteredClaimNames.Exp); // 到期時間
        public DateTime? NotBefore => GetDateTimeClaimValue(JwtRegisteredClaimNames.Nbf); // 生效時間
        public DateTime? IssuedAt => GetDateTimeClaimValue(JwtRegisteredClaimNames.Iat); // 簽發時間
        public string JwtId => GetClaimValue(JwtRegisteredClaimNames.Jti); // JWT ID
        public string? Email => GetClaimValue(JwtRegisteredClaimNames.Email); // 郵箱
        
        // 自定義的 Claim
        public string Role => GetClaimValue("role"); // 角色
        public string PreferredUsername => GetClaimValue("preferred_username"); // 首選用戶名
        public string GivenName => GetClaimValue("given_name"); // 名字

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
