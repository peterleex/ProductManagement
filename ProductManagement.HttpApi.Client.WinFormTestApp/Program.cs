using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            try
            {
                using var application = await AbpApplicationFactory.CreateAsync<ProductManagementWinFormApiClientModule>(options =>
                    {
                        var builder = new ConfigurationBuilder();
                        builder.AddJsonFile("appsettings.json", false);
                        builder.AddJsonFile("appsettings.secrets.json", true);
                        options.Services.ReplaceConfiguration(builder.Build());
                        options.UseAutofac();
                    });

                await application.InitializeAsync();

                var serviceProvider = application.ServiceProvider;

                ApplicationConfiguration.Initialize();

                // 有 CommandLine 參數時，執行更新程式，否則執行 MainForm
                // 判斷執行程式的名稱是不是： LQClientAppUpdator.exe
                if (Process.GetCurrentProcess().ProcessName == LQDefine.UpdateFileName)
                    Application.Run(new LQUpdator(serviceProvider));
                else
                    Application.Run(new MainForm(serviceProvider));

                await application.ShutdownAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                Debug.WriteLine($"An error occurred: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
                // Optionally, rethrow the exception if you want to crash the application
                throw;
            }
        }
    }
}