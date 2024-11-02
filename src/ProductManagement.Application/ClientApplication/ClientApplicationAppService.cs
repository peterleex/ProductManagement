using Microsoft.Extensions.Options;
using ProductManagement;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.ClientApplication
{
    public class ClientApplicationAppService(
        IOptionsMonitor<ClientApplicaionOptions> clientAppSettingsMonitor) : ProductManagementAppService, IClientApplicationAppService
    {
        private readonly IOptionsMonitor<ClientApplicaionOptions> _clientAppSettingsMonitor = clientAppSettingsMonitor;

        public async Task<string> GetDownloadClientAppUrlAsync()
        {
            var clientAppFilePath = _clientAppSettingsMonitor.CurrentValue.ClientAppFilePath;

            var fileUrl = new Uri(clientAppFilePath).ToString();

            return await Task.FromResult(fileUrl);
        }

        public async Task<ClientApplicaionOptions> GetAsync()
        {
            return await Task.FromResult(_clientAppSettingsMonitor.CurrentValue);
        }
    }
}
