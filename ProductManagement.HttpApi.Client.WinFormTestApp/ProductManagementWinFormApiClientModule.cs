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
        //context.Services.AddTransient<IIdentityRoleRepository, IdentityRoleRepository>();
        //context.Services.AddTransient<IIdentityUserRepository, IdentityUserRepository>();
        //context.Services.AddTransient<IOrganizationUnitRepository, OrganizationUnitRepository>();
        //context.Services.AddTransient<IIdentityLinkUserRepository, IdentityLinkUserRepository>();

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
