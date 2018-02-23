using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB
{
    static class Configurator
    {
        public static Config BaseConfiguration()
        {
            return new Config();
        }
    }
}
