using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.GeneratorsOld
{
    public interface IMappingGenerator
    {
        void Generate(Kistl.API.IKistlContext ctx, string path);
    }

    public static class MappingGeneratorFactory
    {
        public static IMappingGenerator GetGenerator()
        {
            return new SQLServer.SQLServerEntityFrameworkModelGenerator();
        }
    }
}
