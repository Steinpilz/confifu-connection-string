using Confifu.Abstractions;
using Confifu.Abstractions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Confifu.ConnectionString.Abstractions
{
    public static class AppConfigExtensions
    {
        public static IAppConfig SetConnectionStringProvider(
            this IAppConfig appConfig, 
            Func<IServiceProvider, IConnectionStringProvider> connectionStringProviderFactory
            )
        {
            if (connectionStringProviderFactory == null)
                throw new ArgumentNullException(nameof(connectionStringProviderFactory));

            return appConfig.RegisterServices(sc =>
            {
                sc.Replace(ServiceDescriptor.Transient(connectionStringProviderFactory));
            });
        }

        public static IAppConfig SetConnectionStringProvider(this IAppConfig appConfig,
            Func<string, string> connectionStringProvider)
            => appConfig.SetConnectionStringProvider(sp => new TransparentConnectionStringProvider(connectionStringProvider));

        public static IAppConfig SetConfigVariablesConnectionStringProvider(this IAppConfig appConfig, string prefix = "")
            => appConfig.SetConnectionStringProvider(sp => new ConfigVariablesConnectionStringProvider(appConfig.GetConfigVariables().WithPrefix(prefix)));
    }
}
