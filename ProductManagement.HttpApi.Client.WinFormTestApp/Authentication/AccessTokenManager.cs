using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Volo.Abp.DependencyInjection;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Authentication
{
    public class AccessTokenManager : ISingletonDependency
    {
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
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                return await httpClient.RequestPasswordTokenAsync(
                    await CreatePasswordTokenRequestAsync(discoveryResponse, userName, password)
                );
            }
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
