using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Volo.Abp.Users;
using System.Net.Http.Json;
using Volo.Abp.Account.Web.Areas.Account.Controllers.Models;

namespace ProductManagement.HttpApi.Client.WinFormTestApp;

public class ClientDemoService : ITransientDependency
{
    private readonly IProfileAppService _profileAppService;
    private readonly IIdentityUserAppService _identityUserAppService;
    private readonly ILogger<ClientDemoService> _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IAccountAppService _accountAppService;
    private readonly IHttpClientFactory _httpClientFactory;
    public ClientDemoService(
        IProfileAppService profileAppService,
        IIdentityUserAppService identityUserAppService,
        ILogger<ClientDemoService> logger,
        ICurrentUser currentUser,
        IAccountAppService accountAppService,
        IHttpClientFactory httpClientFactory
        )
    {
        _profileAppService = profileAppService;
        _identityUserAppService = identityUserAppService;
        _logger = logger;
        _currentUser = currentUser;
        _accountAppService = accountAppService;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<AbpLoginResult?> LoginAsync(UserLoginInfo userLoginInfo)
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client.PostAsJsonAsync("https://localhost:44362/api/account/login", userLoginInfo);
        response.EnsureSuccessStatusCode();

        var loginResult = await response.Content.ReadFromJsonAsync<AbpLoginResult>();

        return loginResult;
    }

    public async Task LogoutAsync()
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync("https://localhost:44362/api/account/logout");
        response.EnsureSuccessStatusCode();
    }

    private class LoginResult
    {
        public int result { get; set; }

        public string description { get; set; } = string.Empty;
    }

    public async Task RunAsync()
    {
        try
        {
            Debug.WriteLine(_currentUser.IsAuthenticated);

            var profileDto = await _profileAppService.GetAsync();
            Debug.WriteLine($"UserName : {profileDto.UserName}");
            Debug.WriteLine($"Email    : {profileDto.Email}");
            Debug.WriteLine($"Name     : {profileDto.Name}");
            Debug.WriteLine($"Surname  : {profileDto.Surname}");

            var resultDto = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
            Debug.WriteLine($"Total users: {resultDto.TotalCount}");
            foreach (var identityUserDto in resultDto.Items)
            {
                Debug.WriteLine($"- [{identityUserDto.Id}] {identityUserDto.Name}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }
}
