using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public interface IDatabaseGenerator
    {
        void Generate(Kistl.API.Server.KistlDataContext ctx);
    }

    public class DatabaseGeneratorFactory
    {
        public static IDatabaseGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDatabaseGenerator();
        }
    }
}
