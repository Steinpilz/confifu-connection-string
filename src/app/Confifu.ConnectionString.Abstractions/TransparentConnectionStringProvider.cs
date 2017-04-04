using Confifu.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Confifu.ConnectionString.Abstractions
{
    public class TransparentConnectionStringProvider : IConnectionStringProvider
    {
        readonly Func<string, string> connectionStringFunc;

        public TransparentConnectionStringProvider(Func<string, string> connectionStringFunc)
        {
            this.connectionStringFunc = connectionStringFunc ?? throw new ArgumentNullException(nameof(connectionStringFunc));
        }

        public string GetConnectionString(string name)
            => connectionStringFunc(name);
    }
}
