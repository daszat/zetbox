using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public interface IMappingGenerator
    {
        void Generate(Kistl.API.Server.KistlDataContext ctx, string path);
    }

    public class MappingGeneratorFactory
    {
        public static IMappingGenerator GetGenerator()
        {
            return new SQLServer.SQLServerEntityFrameworkModelGenerator();
        }
    }
}
