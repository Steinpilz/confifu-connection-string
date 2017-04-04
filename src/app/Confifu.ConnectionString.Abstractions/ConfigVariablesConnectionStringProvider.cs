using Confifu.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Confifu.ConnectionString.Abstractions
{

    public class ConfigVariablesConnectionStringProvider : IConnectionStringProvider
    {
        readonly IConfigVariables configVariables;

        public ConfigVariablesConnectionStringProvider(IConfigVariables configVariables)
        {
            this.configVariables = configVariables ?? throw new ArgumentNullException(nameof(configVariables));
        }

        public string GetConnectionString(string name)
            => PossibleConfigVariablesKeys(name)
            .Select(x => configVariables[x])
            .Where(x => x != null)
            .FirstOrDefault();

        private IEnumerable<string> PossibleConfigVariablesKeys(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                yield return "ConnectionString";
                yield return "ConnectionStrings:Default";
            }
            else
            {
                yield return $"ConnectionStrings:{connectionStringName}";
            }
        }
    }
}
