using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB
{
    public class Connection : IDisposable
    {
        IDriver driver;
        public ISession Session;

        public Connection(string username, string password)
        {
            driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic(username, password), Configurator.BaseConfiguration());
            Session = driver.Session();
        }

        public void Dispose()
        {
            driver.Dispose();
            Session.Dispose();
        }
    }
}
