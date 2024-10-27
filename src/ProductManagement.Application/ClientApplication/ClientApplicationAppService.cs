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
        public async Task<ClientApplicaionSettingDto> GetAsync()
        {
            var clientSetting = new ClientApplicaionSettingDto { ClientAppFilePath = "C:", ClientAppVersion = "1.0.0" };
            return await Task.FromResult(clientSetting);
        }
    }
}
