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
    public class ClientApplicationAppService : ProductManagementAppService, IClientApplicationAppService
    {
        private readonly ClientApplicaionOptions _clientAppSettings;
        private readonly IOptionsMonitor<ClientApplicaionOptions> _clientAppSettingsMonitor;
        private readonly string _selfUrl;

        public ClientApplicationAppService(
            IOptions<ClientApplicaionOptions> clientAppSettings,
            IConfiguration configuration,
            IOptionsMonitor<ClientApplicaionOptions> clientAppSettingsMonitor)
        {
            _clientAppSettings = clientAppSettings.Value;
            _clientAppSettingsMonitor = clientAppSettingsMonitor;
            _selfUrl = configuration["App:SelfUrl"]!;
        }

        public async Task<string> GetDownloadClientAppUrlAsync()
        {
            var clientAppFilePath = _clientAppSettings.ClientAppFilePath;
            
            var fileUrl = new Uri(new Uri(_selfUrl), clientAppFilePath).ToString();

            return await Task.FromResult(fileUrl);
        }

        public async Task<ClientApplicaionOptions> GetAsync()
        {
            return await Task.FromResult(_clientAppSettingsMonitor.CurrentValue);
        }
    }
}
