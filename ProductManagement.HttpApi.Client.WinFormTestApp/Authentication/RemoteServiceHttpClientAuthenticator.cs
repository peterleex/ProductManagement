using System.Threading.Tasks;
using IdentityModel.Client;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.Authentication;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Authentication
{
    public class RemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ITransientDependency
    {
        private readonly AccessTokenManager _accessTokenManager;

        public RemoteServiceHttpClientAuthenticator(AccessTokenManager accessTokenManager)
        {
            _accessTokenManager = accessTokenManager;
        }

        public Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
        {
            context.Client.SetBearerToken(_accessTokenManager.AccessToken);
            return Task.CompletedTask;
        }
    }
}
