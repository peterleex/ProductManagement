using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Volo.Abp.DependencyInjection;
using Serilog;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Authentication
{
    public class AccessTokenManager : ISingletonDependency
    {
        public event EventHandler? AccessTokenObtained;

        public event EventHandler? Logouted;

        public string? AccessToken { get; private set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public AccessTokenManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task ObtainAccessToken(string userName, string password)
        {
            var discoveryResponse = await GetDiscoveryResponse();
            var tokenResponse = await GetTokenResponse(discoveryResponse, userName, password);

            AccessToken = tokenResponse.AccessToken;

            if (AccessToken != null)
            {
                OnAccessTokenObtained();
            }
        }

        protected virtual void OnAccessTokenObtained()
        {
            AccessTokenObtained?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnLogouted()
        {
            Logouted?.Invoke(this, EventArgs.Empty);
        }

        public async Task Logout()
        {
            if (AccessToken != null)
            {
                var discoveryResponse = await GetDiscoveryResponse();
                await RevokeToken(discoveryResponse, AccessToken);
                AccessToken = null;
                OnLogouted();
            }
        }

        protected async Task RevokeToken(DiscoveryDocumentResponse discoveryResponse, string token)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var revokeTokenRequest = new TokenRevocationRequest
                {
                    Address = discoveryResponse.RevocationEndpoint,
                    ClientId = "ProductManagement_App",
                    Token = token,
                    TokenTypeHint = "access_token"
                };

                await httpClient.RevokeTokenAsync(revokeTokenRequest);
            }
        }

        protected async Task<DiscoveryDocumentResponse> GetDiscoveryResponse()
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                return await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = "https://localhost:44362",
                    Policy = { RequireHttps = true }
                });
            }
        }

        protected async Task<TokenResponse> GetTokenResponse(
            DiscoveryDocumentResponse discoveryResponse,
            string userName,
            string password
        )
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(
                await CreatePasswordTokenRequestAsync(discoveryResponse, userName, password)
            );

            if (tokenResponse.IsError)
            {
                var errorMessage = $"Error retrieving token: {tokenResponse.Error}\n" +
                                   $"Error Description: {tokenResponse.ErrorDescription}\n" +
                                   $"HTTP Status Code: {tokenResponse.HttpStatusCode}";


                throw new HttpRequestException(errorMessage, null, tokenResponse.HttpStatusCode);
            }

            return tokenResponse;
        }

        protected virtual Task<PasswordTokenRequest> CreatePasswordTokenRequestAsync(
                    DiscoveryDocumentResponse discoveryResponse,
                    string userName,
                    string password)
        {
            var request = new PasswordTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                Scope = "ProductManagement",
                ClientId = "ProductManagement_App",
                //ClientSecret = "1q2w3e*",
                UserName = userName,
                Password = password
            };

            return Task.FromResult(request);
        }
    }
}
