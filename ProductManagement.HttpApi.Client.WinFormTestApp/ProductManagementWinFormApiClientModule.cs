using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;
using Serilog;
using Volo.Abp.Identity.AspNetCore;
using Autofac.Core;
using Volo.Abp.Identity;
using Volo.Abp.Account.Web;
using Microsoft.Extensions.Configuration;
using Serilog.Settings.Configuration;
using System.Reflection;
using Asp.Versioning;
using System.Configuration;
using System.Diagnostics;

namespace ProductManagement.HttpApi.Client.WinFormTestApp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProductManagementHttpApiClientModule),
    typeof(AbpIdentityAspNetCoreModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
    [DependsOn(typeof(AbpAccountWebModule))]
    public class ProductManagementWinFormApiClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // Get application version
        var appFileVersion = GetAppFileVersion();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build())
                        .Enrich.WithProperty("AppFileVersion", appFileVersion)
                        .CreateLogger();

        // Configure logging
        context.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        PreConfigure<AbpHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
            {
                clientBuilder.AddTransientHttpErrorPolicy(
                    policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                );
            });
        });
    }

    public static string GetAppFileVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        return fileVersionInfo.FileVersion!;
    }
}
