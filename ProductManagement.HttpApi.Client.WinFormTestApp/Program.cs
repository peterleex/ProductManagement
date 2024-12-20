using System.Diagnostics;
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
        static void Main()
        {
            var tcs = new TaskCompletionSource<bool>();

            var thread = new Thread(async () =>
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

                    if (Process.GetCurrentProcess().ProcessName == LQDefine.UpdatorExeFileProcessName ||
                        Environment.GetCommandLineArgs().Length > 1)
                    {
                        Application.Run(new LQUpdator(serviceProvider));
                    }
                    else
                    {
                        Application.Run(new LQHome(serviceProvider));
                    }

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
                finally
                {
                    tcs.SetResult(true);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            tcs.Task.Wait();
        }
    }
}
