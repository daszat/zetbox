using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public interface IMappingGenerator
    {
        void Generate(Kistl.API.IKistlContext ctx, string path);
    }

    public sealed class MappingGeneratorFactory
    {
        public static IMappingGenerator GetGenerator()
        {
            return new SQLServer.SQLServerEntityFrameworkModelGenerator();
        }

        /// <summary>
        /// prevent this class from being instantiated
        /// </summary>
        private MappingGeneratorFactory() { }
    }
}
