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

namespace ProductManagement.HttpApi.Client.WinFormTestApp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProductManagementHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
[DependsOn(typeof(AbpIdentityAspNetCoreModule))]
    public class ProductManagementWinFormApiClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // Configure logging
        context.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
            loggingBuilder.AddSerilog();
            //loggingBuilder.AddFile("Logs/app-{Date}.txt"); // Requires Serilog or other file logging provider
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
}
