using System;

namespace Confifu.ConnectionString.Abstractions
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string name);
    }

    public static class ConnectionStringProviderExts
    {
        public static string GetDefaultConnectionString(this IConnectionStringProvider connectionStringProvider)
            => connectionStringProvider.GetConnectionString("Default");
    }
}
