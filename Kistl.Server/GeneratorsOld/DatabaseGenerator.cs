using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.GeneratorsOld
{
    public interface IDatabaseGenerator
    {
        void Generate(Kistl.API.IKistlContext ctx);
    }

    public static class DatabaseGeneratorFactory
    {
        public static IDatabaseGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDatabaseGenerator();
        }
    }
}
