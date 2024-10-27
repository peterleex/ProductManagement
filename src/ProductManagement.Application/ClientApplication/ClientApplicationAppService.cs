using Microsoft.Extensions.Options;
using ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.ClientApplication
{
    public class ClientApplicationAppService : ProductManagementAppService, IClientApplicationAppService
    {
        private readonly ClientApplicaionOptions _clientAppSettings;
        private readonly IOptionsMonitor<ClientApplicaionOptions> _clientAppSettingsMonitor;
        public ClientApplicationAppService(
            IOptions<ClientApplicaionOptions> clientAppSettings,
            IOptionsMonitor<ClientApplicaionOptions> clientAppSettingsMonitor)
        {
            _clientAppSettings = clientAppSettings.Value;
            _clientAppSettingsMonitor = clientAppSettingsMonitor;
        }


        public async Task<ClientApplicaionOptions> GetAsync()
        {
            //var clientSetting = new ClientApplicaionSettingDto { ClientAppFilePath = "C:", ClientAppVersion = "1.0.0" };
            return await Task.FromResult(_clientAppSettingsMonitor.CurrentValue);
        }
    }
}
