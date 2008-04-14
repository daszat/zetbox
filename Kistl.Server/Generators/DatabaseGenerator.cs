using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public interface IDatabaseGenerator
    {
        void Generate(Kistl.API.IKistlContext ctx);
    }

    public sealed class DatabaseGeneratorFactory
    {
        public static IDatabaseGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDatabaseGenerator();
        }

        /// <summary>
        /// prevent this class from being instatiated
        /// </summary>
        private DatabaseGeneratorFactory() { }
    }
}
