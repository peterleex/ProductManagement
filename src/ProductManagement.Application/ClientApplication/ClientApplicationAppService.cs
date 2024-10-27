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

        public ClientApplicationAppService(IOptions<ClientApplicaionOptions> clientAppSettings)
        {
            _clientAppSettings = clientAppSettings.Value;
        }


        public async Task<ClientApplicaionOptions> GetAsync()
        {
            //var clientSetting = new ClientApplicaionSettingDto { ClientAppFilePath = "C:", ClientAppVersion = "1.0.0" };
            return await Task.FromResult(_clientAppSettings);
        }
    }
}
